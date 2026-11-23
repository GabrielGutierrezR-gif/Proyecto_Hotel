
-- 2. Crear la base de datos limpia
CREATE DATABASE HotelDB;
GO

USE HotelDB;
GO

-- ============================================================================
-- 1. CATÁLOGOS MAESTROS (ESTADOS, DOCUMENTOS, IMPUESTOS, DESCUENTOS, EDADES)
-- ============================================================================

CREATE TABLE Estados (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Entidad VARCHAR(30) NOT NULL,
    Codigo VARCHAR(30) NOT NULL,
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(200) NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT UQ_Estados_Entidad_Codigo UNIQUE (Entidad, Codigo)
);

CREATE TABLE TiposDocumento (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(20) NOT NULL UNIQUE,
    Nombre NVARCHAR(60) NOT NULL UNIQUE,
    LongitudMinima INT NOT NULL DEFAULT 5,
    LongitudMaxima INT NOT NULL DEFAULT 30,
    Activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE TiposCliente (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE,
    Descripcion NVARCHAR(200) NULL,
    PorcentajeDescuentoDefault DECIMAL(5,2) NOT NULL DEFAULT 0 CHECK (PorcentajeDescuentoDefault BETWEEN 0 AND 100),
    Activo BIT NOT NULL DEFAULT 1
);

-- CORREGIDO: Restricción CHECK movida a nivel de tabla
CREATE TABLE PoliticasEdad (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Categoria NVARCHAR(40) NOT NULL UNIQUE,
    EdadMinima INT NOT NULL CHECK (EdadMinima >= 0),
    EdadMaxima INT NOT NULL,
    PorcentajeTarifa DECIMAL(5,2) NOT NULL CHECK (PorcentajeTarifa BETWEEN 0 AND 100),
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT CK_PoliticasEdad_Rango CHECK (EdadMaxima >= EdadMinima)
);

CREATE TABLE Impuestos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(20) NOT NULL UNIQUE,
    Nombre NVARCHAR(60) NOT NULL,
    Porcentaje DECIMAL(5,2) NOT NULL CHECK (Porcentaje >= 0),
    AplicaHospedaje BIT NOT NULL DEFAULT 1,
    AplicaRestaurante BIT NOT NULL DEFAULT 1,
    Activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Descuentos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(30) NOT NULL UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    TipoValor VARCHAR(15) NOT NULL CHECK (TipoValor IN ('Porcentaje', 'MontoFijo')),
    Valor DECIMAL(18,2) NOT NULL CHECK (Valor > 0),
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    AplicaHospedaje BIT NOT NULL DEFAULT 1,
    AplicaRestaurante BIT NOT NULL DEFAULT 0,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT CK_Descuentos_Fechas CHECK (FechaInicio <= FechaFin)
);

-- ============================================================================
-- 2. SEGURIDAD (RBAC + JWT) Y CLIENTES
-- ============================================================================

CREATE TABLE Roles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Permisos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(60) NOT NULL UNIQUE
);

CREATE TABLE RolPermisos (
    RolId INT NOT NULL REFERENCES Roles(Id),
    PermisoId INT NOT NULL REFERENCES Permisos(Id),
    PRIMARY KEY (RolId, PermisoId)
);

CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TipoDocumentoId INT NOT NULL REFERENCES TiposDocumento(Id),
    NumeroDocumento VARCHAR(30) NOT NULL,
    TipoClienteId INT NOT NULL REFERENCES TiposCliente(Id),
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    Nacionalidad NVARCHAR(60) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Telefono VARCHAR(20) NULL,
    Preferencias NVARCHAR(500) NULL,
    FechaRegistroUtc DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT UQ_Clientes_Documento UNIQUE (TipoDocumentoId, NumeroDocumento)
);

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    NombreCompleto NVARCHAR(200) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacionUtc DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    ClienteId INT NULL REFERENCES Clientes(Id)
);

CREATE TABLE UsuarioRoles (
    UsuarioId INT NOT NULL REFERENCES Usuarios(Id),
    RolId INT NOT NULL REFERENCES Roles(Id),
    PRIMARY KEY (UsuarioId, RolId)
);

CREATE TABLE RefreshTokens (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL REFERENCES Usuarios(Id),
    TokenHash VARCHAR(128) NOT NULL UNIQUE,
    FechaCreacionUtc DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    FechaExpiracionUtc DATETIME2(0) NOT NULL,
    FechaRevocacionUtc DATETIME2(0) NULL,
    TokenReemplazoId INT NULL REFERENCES RefreshTokens(Id)
);

-- ============================================================================
-- 3. HABITACIONES, CAMAS, AMENIDADES Y TARIFAS POR TEMPORADA
-- ============================================================================

-- CORREGIDO: Restricción CHECK movida a nivel de tabla
CREATE TABLE TiposHabitacion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE,
    Descripcion NVARCHAR(500) NULL,
    CapacidadBaseAdultos INT NOT NULL CHECK (CapacidadBaseAdultos > 0),
    CapacidadMaximaAdultos INT NOT NULL,
    CapacidadMaximaNinos INT NOT NULL CHECK (CapacidadMaximaNinos >= 0),
    PrecioBasePorNoche DECIMAL(18,2) NOT NULL CHECK (PrecioBasePorNoche > 0),
    CargoPersonaAdicional DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (CargoPersonaAdicional >= 0),
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT CK_TiposHabitacion_Capacidad CHECK (CapacidadMaximaAdultos >= CapacidadBaseAdultos)
);

CREATE TABLE TiposCama (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE,
    CapacidadPersonas INT NOT NULL CHECK (CapacidadPersonas > 0),
    Activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Amenidades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(60) NOT NULL UNIQUE,
    Activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Habitaciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Numero VARCHAR(10) NOT NULL UNIQUE,
    Piso INT NOT NULL CHECK (Piso >= 0),
    TipoHabitacionId INT NOT NULL REFERENCES TiposHabitacion(Id),
    EstadoId INT NOT NULL REFERENCES Estados(Id),
    PrecioPorNoche DECIMAL(18,2) NOT NULL CHECK (PrecioPorNoche > 0),
    ImagenUrl NVARCHAR(400) NULL,
    Activo BIT NOT NULL DEFAULT 1,
    RowVersion ROWVERSION
);

CREATE TABLE HabitacionCamas (
    HabitacionId INT NOT NULL REFERENCES Habitaciones(Id),
    TipoCamaId INT NOT NULL REFERENCES TiposCama(Id),
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PRIMARY KEY (HabitacionId, TipoCamaId)
);

CREATE TABLE HabitacionAmenidades (
    HabitacionId INT NOT NULL REFERENCES Habitaciones(Id),
    AmenidadId INT NOT NULL REFERENCES Amenidades(Id),
    PRIMARY KEY (HabitacionId, AmenidadId)
);

CREATE TABLE HistorialEstadoHabitacion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    HabitacionId INT NOT NULL REFERENCES Habitaciones(Id),
    EstadoAnteriorId INT NOT NULL REFERENCES Estados(Id),
    EstadoNuevoId INT NOT NULL REFERENCES Estados(Id),
    UsuarioId INT NULL REFERENCES Usuarios(Id),
    Motivo NVARCHAR(300) NULL,
    FechaUtc DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE Temporadas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(60) NOT NULL,
    TipoTemporada VARCHAR(15) NOT NULL CHECK (TipoTemporada IN ('Baja', 'Media', 'Alta')),
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    PorcentajeAjuste DECIMAL(6,2) NOT NULL DEFAULT 0,
    PorcentajeRecargoFinSemana DECIMAL(5,2) NOT NULL DEFAULT 0 CHECK (PorcentajeRecargoFinSemana >= 0),
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT CK_Temporadas_Fechas CHECK (FechaInicio <= FechaFin)
);

-- ============================================================================
-- 4. RESERVACIONES Y HUÉSPEDES
-- ============================================================================

CREATE SEQUENCE SeqReservacion AS INT START WITH 1 INCREMENT BY 1;
GO

CREATE TABLE Reservaciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(20) NOT NULL UNIQUE,
    ClienteId INT NOT NULL REFERENCES Clientes(Id),
    TipoClienteId INT NOT NULL REFERENCES TiposCliente(Id),
    TemporadaId INT NULL REFERENCES Temporadas(Id),
    DescuentoId INT NULL REFERENCES Descuentos(Id),
    ImpuestoId INT NOT NULL REFERENCES Impuestos(Id),
    EstadoId INT NOT NULL REFERENCES Estados(Id),
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    CantidadAdultos INT NOT NULL CHECK (CantidadAdultos > 0),
    CantidadNinos INT NOT NULL DEFAULT 0 CHECK (CantidadNinos >= 0),
    Subtotal DECIMAL(18,2) NOT NULL CHECK (Subtotal >= 0),
    MontoDescuento DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (MontoDescuento >= 0),
    MontoImpuestos DECIMAL(18,2) NOT NULL CHECK (MontoImpuestos >= 0),
    Total DECIMAL(18,2) NOT NULL CHECK (Total >= 0),
    AnticipoRequerido DECIMAL(18,2) NOT NULL CHECK (AnticipoRequerido >= 0),
    Observaciones NVARCHAR(500) NULL,
    MotivoCancelacion NVARCHAR(300) NULL,
    FechaCreacionUtc DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    RowVersion ROWVERSION,
    CONSTRAINT CK_Reservaciones_Fechas CHECK (FechaInicio < FechaFin)
);

CREATE TABLE ReservacionHabitaciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReservacionId INT NOT NULL REFERENCES Reservaciones(Id),
    HabitacionId INT NOT NULL REFERENCES Habitaciones(Id),
    TipoClienteId INT NOT NULL REFERENCES TiposCliente(Id),
    ClienteResponsableId INT NOT NULL REFERENCES Clientes(Id),
    TarifaBaseHabitacion DECIMAL(18,2) NOT NULL CHECK (TarifaBaseHabitacion > 0),
    AjusteTemporada DECIMAL(18,2) NOT NULL DEFAULT 0,
    CargoHuespedesAdicionales DECIMAL(18,2) NOT NULL DEFAULT 0,
    TarifaFinalNoche DECIMAL(18,2) NOT NULL CHECK (TarifaFinalNoche > 0),
    CONSTRAINT UQ_ResHab UNIQUE (ReservacionId, HabitacionId)
);

CREATE TABLE HuespedesReservacion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReservacionId INT NOT NULL REFERENCES Reservaciones(Id),
    HabitacionId INT NOT NULL REFERENCES Habitaciones(Id),
    ClienteTitularReferenteId INT NOT NULL REFERENCES Clientes(Id),
    TipoDocumentoId INT NOT NULL REFERENCES TiposDocumento(Id),
    NumeroDocumento VARCHAR(30) NOT NULL,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    PoliticaEdadId INT NOT NULL REFERENCES PoliticasEdad(Id),
    TarifaAplicadaHuesped DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (TarifaAplicadaHuesped >= 0)
);

CREATE TABLE HistorialReservacion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReservacionId INT NOT NULL REFERENCES Reservaciones(Id),
    EstadoAnteriorId INT NULL REFERENCES Estados(Id),
    EstadoNuevoId INT NOT NULL REFERENCES Estados(Id),
    UsuarioId INT NULL REFERENCES Usuarios(Id),
    Motivo NVARCHAR(300) NULL,
    FechaUtc DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME()
);

-- ============================================================================
-- 5. ESTADÍAS, CARGOS ADICIONALES, PAGOS Y FACTURACIÓN
-- ============================================================================

CREATE TABLE Estadias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReservacionId INT NOT NULL UNIQUE REFERENCES Reservaciones(Id),
    CheckInRealUtc DATETIME2(0) NOT NULL,
    CheckOutRealUtc DATETIME2(0) NULL,
    UsuarioCheckInId INT NOT NULL REFERENCES Usuarios(Id),
    UsuarioCheckOutId INT NULL REFERENCES Usuarios(Id),
    CodigoAcceso VARCHAR(20) NULL,
    CONSTRAINT CK_Estadias_Fechas CHECK (CheckOutRealUtc IS NULL OR CheckOutRealUtc > CheckInRealUtc)
);

CREATE TABLE CargosAdicionales (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReservacionId INT NOT NULL REFERENCES Reservaciones(Id),
    Tipo VARCHAR(20) NOT NULL CHECK (Tipo IN ('LateCheckOut', 'ServicioAdicional', 'Ajuste')),
    Descripcion NVARCHAR(200) NOT NULL,
    Subtotal DECIMAL(18,2) NOT NULL,
    ImpuestoId INT NULL REFERENCES Impuestos(Id),
    MontoImpuesto DECIMAL(18,2) NOT NULL DEFAULT 0,
    Total DECIMAL(18,2) NOT NULL,
    Justificacion NVARCHAR(300) NULL,
    UsuarioId INT NOT NULL REFERENCES Usuarios(Id),
    FechaUtc DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT CK_Cargos_Ajuste CHECK (Tipo <> 'Ajuste' OR Justificacion IS NOT NULL)
);

CREATE TABLE Pagos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReservacionId INT NOT NULL REFERENCES Reservaciones(Id),
    EstadoId INT NOT NULL REFERENCES Estados(Id),
    Tipo VARCHAR(10) NOT NULL DEFAULT 'Abono' CHECK (Tipo IN ('Abono', 'Reverso')),
    Monto DECIMAL(18,2) NOT NULL CHECK (Monto > 0),
    MetodoPago VARCHAR(20) NOT NULL CHECK (MetodoPago IN ('Efectivo', 'Tarjeta', 'Transferencia')),
    Referencia VARCHAR(100) NULL,
    UltimosCuatro CHAR(4) NULL,
    PagoOrigenId INT NULL REFERENCES Pagos(Id),
    UsuarioId INT NULL REFERENCES Usuarios(Id),
    FechaPagoUtc DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE Facturas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NumeroFactura VARCHAR(20) NOT NULL UNIQUE,
    ReservacionId INT NOT NULL REFERENCES Reservaciones(Id),
    ClienteId INT NOT NULL REFERENCES Clientes(Id),
    DescuentoId INT NULL REFERENCES Descuentos(Id),
    ImpuestoId INT NOT NULL REFERENCES Impuestos(Id),
    Subtotal DECIMAL(18,2) NOT NULL,
    MontoDescuento DECIMAL(18,2) NOT NULL DEFAULT 0,
    MontoImpuestos DECIMAL(18,2) NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    UsuarioId INT NOT NULL REFERENCES Usuarios(Id),
    FechaEmisionUtc DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE DetalleFacturas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FacturaId INT NOT NULL REFERENCES Facturas(Id),
    Concepto NVARCHAR(200) NOT NULL,
    Cantidad INT NOT NULL DEFAULT 1 CHECK (Cantidad > 0),
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    MontoDescuento DECIMAL(18,2) NOT NULL DEFAULT 0,
    MontoImpuesto DECIMAL(18,2) NOT NULL DEFAULT 0,
    TotalLinea DECIMAL(18,2) NOT NULL
);

-- ============================================================================
-- 6. RESTAURANTE / BAR Y PEDIDOS
-- ============================================================================

CREATE TABLE CategoriasPlatillo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE,
    Activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Platillos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CategoriaId INT NOT NULL REFERENCES CategoriasPlatillo(Id),
    Nombre NVARCHAR(100) NOT NULL UNIQUE,
    Descripcion NVARCHAR(500) NULL,
    Precio DECIMAL(18,2) NOT NULL CHECK (Precio > 0),
    TiempoPreparacionMin INT NOT NULL CHECK (TiempoPreparacionMin > 0),
    ImagenUrl NVARCHAR(400) NULL,
    Disponible BIT NOT NULL DEFAULT 1,
    Activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE PedidosRestaurante (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId INT NOT NULL REFERENCES Clientes(Id),
    ReservacionId INT NULL REFERENCES Reservaciones(Id),
    HabitacionId INT NULL REFERENCES Habitaciones(Id),
    EstadoId INT NOT NULL REFERENCES Estados(Id),
    DescuentoId INT NULL REFERENCES Descuentos(Id),
    ImpuestoId INT NOT NULL REFERENCES Impuestos(Id),
    Modalidad VARCHAR(15) NOT NULL CHECK (Modalidad IN ('RoomService', 'Mesa', 'Piscina')),
    CargoAHabitacion BIT NOT NULL DEFAULT 0,
    FechaPedidoUtc DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    FechaHoraDeseadaUtc DATETIME2(0) NULL,
    Instrucciones NVARCHAR(500) NULL,
    Subtotal DECIMAL(18,2) NOT NULL CHECK (Subtotal >= 0),
    MontoDescuento DECIMAL(18,2) NOT NULL DEFAULT 0,
    Recargo DECIMAL(18,2) NOT NULL DEFAULT 0,
    MontoImpuestos DECIMAL(18,2) NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    FechaListoUtc DATETIME2(0) NULL,
    RowVersion ROWVERSION,
    CONSTRAINT CK_Pedidos_CargoHabitacion CHECK (CargoAHabitacion = 0 OR ReservacionId IS NOT NULL)
);

CREATE TABLE DetallePedidos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PedidoId INT NOT NULL REFERENCES PedidosRestaurante(Id),
    PlatilloId INT NOT NULL REFERENCES Platillos(Id),
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PrecioUnitario DECIMAL(18,2) NOT NULL CHECK (PrecioUnitario > 0),
    Subtotal DECIMAL(18,2) NOT NULL
);

-- ============================================================================
-- 7. AUDITORÍA GENERAL DEL SISTEMA
-- ============================================================================

CREATE TABLE Auditoria (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NULL REFERENCES Usuarios(Id),
    Accion VARCHAR(50) NOT NULL,
    Entidad VARCHAR(50) NOT NULL,
    EntidadId INT NOT NULL,
    FechaUtc DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    Datos NVARCHAR(MAX) NULL
);
GO

-- ============================================================================
-- 8. DATOS INICIALES (SEED DATA)
-- ============================================================================

INSERT INTO Estados (Entidad, Codigo, Nombre) VALUES
('Habitacion', 'Disponible', 'Disponible'),
('Habitacion', 'Reservada', 'Reservada'),
('Habitacion', 'Ocupada', 'Ocupada'),
('Habitacion', 'EnLimpieza', 'En Limpieza'),
('Habitacion', 'EnMantenimiento', 'En Mantenimiento'),
('Habitacion', 'Bloqueada', 'Bloqueada'),
('Reservacion', 'PendientePago', 'Pendiente de Pago'),
('Reservacion', 'Confirmada', 'Confirmada'),
('Reservacion', 'EnCurso', 'En Curso'),
('Reservacion', 'Completada', 'Completada'),
('Reservacion', 'Cancelada', 'Cancelada'),
('Pago', 'Confirmado', 'Confirmado'),
('Pago', 'Rechazado', 'Rechazado'),
('Pedido', 'Pendiente', 'Pendiente'),
('Pedido', 'Confirmado', 'Confirmado'),
('Pedido', 'EnPreparacion', 'En Preparación'),
('Pedido', 'Listo', 'Listo'),
('Pedido', 'EnCamino', 'En Camino'),
('Pedido', 'Entregado', 'Entregado'),
('Pedido', 'Cancelado', 'Cancelado');

INSERT INTO TiposDocumento (Codigo, Nombre, LongitudMinima, LongitudMaxima) VALUES
('CEDULA', 'Cédula Nacional', 9, 12),
('DIMEX', 'DIMEX / Residencia', 11, 12),
('PASAPORTE', 'Pasaporte', 6, 20),
('DNI', 'Documento Nacional de Identidad', 8, 15);

INSERT INTO TiposCliente (Nombre, Descripcion, PorcentajeDescuentoDefault) VALUES
('Regular', 'Cliente particular estándar', 0.00),
('Corporativo', 'Convenio empresarial con tarifa preferencial', 10.00),
('VIP', 'Cliente frecuente con beneficios exclusivos', 15.00),
('Agencia', 'Operador turístico o agencia de viajes', 12.00);

INSERT INTO PoliticasEdad (Categoria, EdadMinima, EdadMaxima, PorcentajeTarifa) VALUES
('Infante', 0, 3, 0.00),
('Nino', 4, 12, 50.00),
('Adulto', 13, 120, 100.00);

INSERT INTO Impuestos (Codigo, Nombre, Porcentaje, AplicaHospedaje, AplicaRestaurante) VALUES
('IVA_13', 'IVA General (13%)', 13.00, 1, 1),
('IVA_16', 'IVA Estándar (16%)', 16.00, 1, 1),
('EXENTO', 'Exento de Impuestos', 0.00, 1, 1);

INSERT INTO Descuentos (Codigo, Nombre, TipoValor, Valor, FechaInicio, FechaFin, AplicaHospedaje, AplicaRestaurante) VALUES
('SIN_DESC', 'Sin Descuento', 'Porcentaje', 0.01, '2026-01-01', '2030-12-31', 1, 1),
('BIENVENIDA10', 'Descuento Primera Reserva 10%', 'Porcentaje', 10.00, '2026-01-01', '2026-12-31', 1, 0);

INSERT INTO Roles (Nombre) VALUES
('Admin'), ('Recepcionista'), ('Auditor'), ('PersonalCocina'), ('Huesped'), ('Cajero'), ('Supervisor');

INSERT INTO TiposCama (Nombre, CapacidadPersonas) VALUES
('Individual', 1),
('Matrimonial', 2),
('Queen', 2),
('King', 2),
('Sofá Cama', 1);

INSERT INTO TiposHabitacion (Nombre, Descripcion, CapacidadBaseAdultos, CapacidadMaximaAdultos, CapacidadMaximaNinos, PrecioBasePorNoche, CargoPersonaAdicional) VALUES
('Simple', 'Habitación individual funcional', 1, 1, 0, 60.00, 0.00),
('Doble Estándar', 'Habitación cómoda para parejas o dos personas', 2, 2, 1, 95.00, 20.00),
('Suite Ejecutiva', 'Espacio amplio con área de trabajo', 2, 3, 1, 150.00, 30.00),
('Suite Presidencial', 'Máximo lujo con jacuzzi y vista panorámica', 2, 4, 2, 320.00, 45.00),
('Familiar', 'Ideal para familias numerosas', 4, 4, 2, 180.00, 25.00);

INSERT INTO Amenidades (Nombre) VALUES
('Wi-Fi Alta Velocidad'), ('Aire Acondicionado'), ('Vista al Mar'), ('Balcón Privado'), ('Jacuzzi'), ('Minibar'), ('Caja Fuerte');

INSERT INTO Temporadas (Nombre, TipoTemporada, FechaInicio, FechaFin, PorcentajeAjuste, PorcentajeRecargoFinSemana) VALUES
('Temporada Baja 2026', 'Baja', '2026-05-01', '2026-11-30', -10.00, 10.00),
('Temporada Alta - Fin de Año', 'Alta', '2026-12-01', '2027-01-15', 25.00, 15.00);

INSERT INTO CategoriasPlatillo (Nombre) VALUES
('Entradas'), ('Platos Fuertes'), ('Bebidas'), ('Postres');
GO