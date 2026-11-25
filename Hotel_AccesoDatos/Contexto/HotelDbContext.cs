using System;
using System.Collections.Generic;
using Hotel_AccesoDatos;
using Microsoft.EntityFrameworkCore;

namespace Hotel_AccesoDatos.Contexto;

public partial class HotelDbContext : DbContext
{
    public HotelDbContext()
    {
    }

    public HotelDbContext(DbContextOptions<HotelDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Amenidade> Amenidades { get; set; }

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<CargosAdicionale> CargosAdicionales { get; set; }

    public virtual DbSet<CategoriasPlatillo> CategoriasPlatillos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Descuento> Descuentos { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Estadia> Estadias { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<HabitacionCama> HabitacionCamas { get; set; }

    public virtual DbSet<Habitacione> Habitaciones { get; set; }

    public virtual DbSet<HistorialEstadoHabitacion> HistorialEstadoHabitacions { get; set; }

    public virtual DbSet<HistorialReservacion> HistorialReservacions { get; set; }

    public virtual DbSet<HuespedesReservacion> HuespedesReservacions { get; set; }

    public virtual DbSet<Impuesto> Impuestos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<PedidosRestaurante> PedidosRestaurantes { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Platillo> Platillos { get; set; }

    public virtual DbSet<PoliticasEdad> PoliticasEdads { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<ReservacionHabitacione> ReservacionHabitaciones { get; set; }

    public virtual DbSet<Reservacione> Reservaciones { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Temporada> Temporadas { get; set; }

    public virtual DbSet<TiposCama> TiposCamas { get; set; }

    public virtual DbSet<TiposCliente> TiposClientes { get; set; }

    public virtual DbSet<TiposDocumento> TiposDocumentos { get; set; }

    public virtual DbSet<TiposHabitacion> TiposHabitacions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Amenidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Amenidad__3214EC07D2E9695A");

            entity.HasIndex(e => e.Nombre, "UQ__Amenidad__75E3EFCF4880CD5A").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(60);
        });

        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Auditori__3214EC0747E083AF");

            entity.Property(e => e.Accion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Entidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Auditoria__Usuar__3A179ED3");
        });

        modelBuilder.Entity<CargosAdicionale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CargosAd__3214EC07CD046B41");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Justificacion).HasMaxLength(300);
            entity.Property(e => e.MontoImpuesto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Impuesto).WithMany(p => p.CargosAdicionales)
                .HasForeignKey(d => d.ImpuestoId)
                .HasConstraintName("FK__CargosAdi__Impue__793DFFAF");

            entity.HasOne(d => d.Reservacion).WithMany(p => p.CargosAdicionales)
                .HasForeignKey(d => d.ReservacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CargosAdi__Reser__7755B73D");

            entity.HasOne(d => d.Usuario).WithMany(p => p.CargosAdicionales)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CargosAdi__Usuar__7B264821");
        });

        modelBuilder.Entity<CategoriasPlatillo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07578731DD");

            entity.ToTable("CategoriasPlatillo");

            entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCFF4E0BCBF").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC07E922679D");

            entity.HasIndex(e => new { e.TipoDocumentoId, e.NumeroDocumento }, "UQ_Clientes_Documento").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Clientes__A9D1053499363EC2").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Apellidos).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FechaRegistroUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Nacionalidad).HasMaxLength(60);
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Preferencias).HasMaxLength(500);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.TipoCliente).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.TipoClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clientes__TipoCl__7D439ABD");

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clientes__TipoDo__7C4F7684");
        });

        modelBuilder.Entity<Descuento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Descuent__3214EC07758E8888");

            entity.HasIndex(e => e.Codigo, "UQ__Descuent__06370DAC1C876592").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.AplicaHospedaje).HasDefaultValue(true);
            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.TipoValor)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetalleF__3214EC07FB559170");

            entity.Property(e => e.Cantidad).HasDefaultValue(1);
            entity.Property(e => e.Concepto).HasMaxLength(200);
            entity.Property(e => e.MontoDescuento).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MontoImpuesto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalLinea).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Factura).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.FacturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleFa__Factu__13F1F5EB");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetalleP__3214EC07A8F1DE69");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Pedido).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePe__Pedid__345EC57D");

            entity.HasOne(d => d.Platillo).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.PlatilloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallePe__Plati__3552E9B6");
        });

        modelBuilder.Entity<Estadia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estadias__3214EC07F08A3869");

            entity.HasIndex(e => e.ReservacionId, "UQ__Estadias__145B3ED4177971F4").IsUnique();

            entity.Property(e => e.CheckInRealUtc).HasPrecision(0);
            entity.Property(e => e.CheckOutRealUtc).HasPrecision(0);
            entity.Property(e => e.CodigoAcceso)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Reservacion).WithOne(p => p.Estadia)
                .HasForeignKey<Estadia>(d => d.ReservacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estadias__Reserv__719CDDE7");

            entity.HasOne(d => d.UsuarioCheckIn).WithMany(p => p.EstadiaUsuarioCheckIns)
                .HasForeignKey(d => d.UsuarioCheckInId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estadias__Usuari__72910220");

            entity.HasOne(d => d.UsuarioCheckOut).WithMany(p => p.EstadiaUsuarioCheckOuts)
                .HasForeignKey(d => d.UsuarioCheckOutId)
                .HasConstraintName("FK__Estadias__Usuari__73852659");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estados__3214EC075C5BB1F6");

            entity.HasIndex(e => new { e.Entidad, e.Codigo }, "UQ_Estados_Entidad_Codigo").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Entidad)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facturas__3214EC07634CA615");

            entity.HasIndex(e => e.NumeroFactura, "UQ__Facturas__CF12F9A6EE1D7ABA").IsUnique();

            entity.Property(e => e.FechaEmisionUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.MontoDescuento).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MontoImpuestos).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NumeroFactura)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas__Client__0C50D423");

            entity.HasOne(d => d.Descuento).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.DescuentoId)
                .HasConstraintName("FK__Facturas__Descue__0D44F85C");

            entity.HasOne(d => d.Impuesto).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ImpuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas__Impues__0E391C95");

            entity.HasOne(d => d.Reservacion).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ReservacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas__Reserv__0B5CAFEA");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facturas__Usuari__10216507");
        });

        modelBuilder.Entity<HabitacionCama>(entity =>
        {
            entity.HasKey(e => new { e.HabitacionId, e.TipoCamaId }).HasName("PK__Habitaci__6BC379997A467C9B");

            entity.HasOne(d => d.Habitacion).WithMany(p => p.HabitacionCamas)
                .HasForeignKey(d => d.HabitacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__Habit__2BFE89A6");

            entity.HasOne(d => d.TipoCama).WithMany(p => p.HabitacionCamas)
                .HasForeignKey(d => d.TipoCamaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__TipoC__2CF2ADDF");
        });

        modelBuilder.Entity<Habitacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Habitaci__3214EC07ECFED6F2");

            entity.HasIndex(e => e.Numero, "UQ__Habitaci__7E532BC630B38537").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ImagenUrl).HasMaxLength(400);
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PrecioPorNoche).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Estado).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__Estad__2739D489");

            entity.HasOne(d => d.TipoHabitacion).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.TipoHabitacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__TipoH__2645B050");

            entity.HasMany(d => d.Amenidads).WithMany(p => p.Habitacions)
                .UsingEntity<Dictionary<string, object>>(
                    "HabitacionAmenidade",
                    r => r.HasOne<Amenidade>().WithMany()
                        .HasForeignKey("AmenidadId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Habitacio__Ameni__31B762FC"),
                    l => l.HasOne<Habitacione>().WithMany()
                        .HasForeignKey("HabitacionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Habitacio__Habit__30C33EC3"),
                    j =>
                    {
                        j.HasKey("HabitacionId", "AmenidadId").HasName("PK__Habitaci__433D3AED8DD7C100");
                        j.ToTable("HabitacionAmenidades");
                    });
        });

        modelBuilder.Entity<HistorialEstadoHabitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC0782717E28");

            entity.ToTable("HistorialEstadoHabitacion");

            entity.Property(e => e.FechaUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Motivo).HasMaxLength(300);

            entity.HasOne(d => d.EstadoAnterior).WithMany(p => p.HistorialEstadoHabitacionEstadoAnteriors)
                .HasForeignKey(d => d.EstadoAnteriorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Estad__3587F3E0");

            entity.HasOne(d => d.EstadoNuevo).WithMany(p => p.HistorialEstadoHabitacionEstadoNuevos)
                .HasForeignKey(d => d.EstadoNuevoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Estad__367C1819");

            entity.HasOne(d => d.Habitacion).WithMany(p => p.HistorialEstadoHabitacions)
                .HasForeignKey(d => d.HabitacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Habit__3493CFA7");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialEstadoHabitacions)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Historial__Usuar__37703C52");
        });

        modelBuilder.Entity<HistorialReservacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC072F0C7805");

            entity.ToTable("HistorialReservacion");

            entity.Property(e => e.FechaUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Motivo).HasMaxLength(300);

            entity.HasOne(d => d.EstadoAnterior).WithMany(p => p.HistorialReservacionEstadoAnteriors)
                .HasForeignKey(d => d.EstadoAnteriorId)
                .HasConstraintName("FK__Historial__Estad__6AEFE058");

            entity.HasOne(d => d.EstadoNuevo).WithMany(p => p.HistorialReservacionEstadoNuevos)
                .HasForeignKey(d => d.EstadoNuevoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Estad__6BE40491");

            entity.HasOne(d => d.Reservacion).WithMany(p => p.HistorialReservacions)
                .HasForeignKey(d => d.ReservacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Reser__69FBBC1F");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialReservacions)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Historial__Usuar__6CD828CA");
        });

        modelBuilder.Entity<HuespedesReservacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Huespede__3214EC07462722A6");

            entity.ToTable("HuespedesReservacion");

            entity.Property(e => e.Apellidos).HasMaxLength(100);
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TarifaAplicadaHuesped).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ClienteTitularReferente).WithMany(p => p.HuespedesReservacions)
                .HasForeignKey(d => d.ClienteTitularReferenteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Huespedes__Clien__634EBE90");

            entity.HasOne(d => d.Habitacion).WithMany(p => p.HuespedesReservacions)
                .HasForeignKey(d => d.HabitacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Huespedes__Habit__625A9A57");

            entity.HasOne(d => d.PoliticaEdad).WithMany(p => p.HuespedesReservacions)
                .HasForeignKey(d => d.PoliticaEdadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Huespedes__Polit__65370702");

            entity.HasOne(d => d.Reservacion).WithMany(p => p.HuespedesReservacions)
                .HasForeignKey(d => d.ReservacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Huespedes__Reser__6166761E");

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.HuespedesReservacions)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Huespedes__TipoD__6442E2C9");
        });

        modelBuilder.Entity<Impuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Impuesto__3214EC07692CB737");

            entity.HasIndex(e => e.Codigo, "UQ__Impuesto__06370DACA484F844").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.AplicaHospedaje).HasDefaultValue(true);
            entity.Property(e => e.AplicaRestaurante).HasDefaultValue(true);
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre).HasMaxLength(60);
            entity.Property(e => e.Porcentaje).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagos__3214EC078D657D24");

            entity.Property(e => e.FechaPagoUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Referencia)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("Abono");
            entity.Property(e => e.UltimosCuatro)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Estado).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__EstadoId__00DF2177");

            entity.HasOne(d => d.PagoOrigen).WithMany(p => p.InversePagoOrigen)
                .HasForeignKey(d => d.PagoOrigenId)
                .HasConstraintName("FK__Pagos__PagoOrige__05A3D694");

            entity.HasOne(d => d.Reservacion).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.ReservacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__Reservaci__7FEAFD3E");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Pagos__UsuarioId__0697FACD");
        });

        modelBuilder.Entity<PedidosRestaurante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PedidosR__3214EC0797BCDD8F");

            entity.ToTable("PedidosRestaurante");

            entity.Property(e => e.CargoAhabitacion).HasColumnName("CargoAHabitacion");
            entity.Property(e => e.FechaHoraDeseadaUtc).HasPrecision(0);
            entity.Property(e => e.FechaListoUtc).HasPrecision(0);
            entity.Property(e => e.FechaPedidoUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Instrucciones).HasMaxLength(500);
            entity.Property(e => e.Modalidad)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.MontoDescuento).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MontoImpuestos).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Recargo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.PedidosRestaurantes)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PedidosRe__Clien__2610A626");

            entity.HasOne(d => d.Descuento).WithMany(p => p.PedidosRestaurantes)
                .HasForeignKey(d => d.DescuentoId)
                .HasConstraintName("FK__PedidosRe__Descu__29E1370A");

            entity.HasOne(d => d.Estado).WithMany(p => p.PedidosRestaurantes)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PedidosRe__Estad__28ED12D1");

            entity.HasOne(d => d.Habitacion).WithMany(p => p.PedidosRestaurantes)
                .HasForeignKey(d => d.HabitacionId)
                .HasConstraintName("FK__PedidosRe__Habit__27F8EE98");

            entity.HasOne(d => d.Impuesto).WithMany(p => p.PedidosRestaurantes)
                .HasForeignKey(d => d.ImpuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PedidosRe__Impue__2AD55B43");

            entity.HasOne(d => d.Reservacion).WithMany(p => p.PedidosRestaurantes)
                .HasForeignKey(d => d.ReservacionId)
                .HasConstraintName("FK__PedidosRe__Reser__2704CA5F");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permisos__3214EC07786D3091");

            entity.HasIndex(e => e.Codigo, "UQ__Permisos__06370DACC6DDC089").IsUnique();

            entity.Property(e => e.Codigo)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Platillo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platillo__3214EC07A93F7070");

            entity.HasIndex(e => e.Nombre, "UQ__Platillo__75E3EFCFA51C0AD5").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Disponible).HasDefaultValue(true);
            entity.Property(e => e.ImagenUrl).HasMaxLength(400);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Platillos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Platillos__Categ__1F63A897");
        });

        modelBuilder.Entity<PoliticasEdad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Politica__3214EC07CB1D3E1D");

            entity.ToTable("PoliticasEdad");

            entity.HasIndex(e => e.Categoria, "UQ__Politica__08015F8BAB3D4BD1").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Categoria).HasMaxLength(40);
            entity.Property(e => e.PorcentajeTarifa).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC074836AA58");

            entity.HasIndex(e => e.TokenHash, "UQ__RefreshT__BCB33F92006949EA").IsUnique();

            entity.Property(e => e.FechaCreacionUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.FechaExpiracionUtc).HasPrecision(0);
            entity.Property(e => e.FechaRevocacionUtc).HasPrecision(0);
            entity.Property(e => e.TokenHash)
                .HasMaxLength(128)
                .IsUnicode(false);

            entity.HasOne(d => d.TokenReemplazo).WithMany(p => p.InverseTokenReemplazo)
                .HasForeignKey(d => d.TokenReemplazoId)
                .HasConstraintName("FK__RefreshTo__Token__0F624AF8");

            entity.HasOne(d => d.Usuario).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RefreshTo__Usuar__0D7A0286");
        });

        modelBuilder.Entity<ReservacionHabitacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservac__3214EC076A76ADFF");

            entity.HasIndex(e => new { e.ReservacionId, e.HabitacionId }, "UQ_ResHab").IsUnique();

            entity.Property(e => e.AjusteTemporada).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CargoHuespedesAdicionales).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TarifaBaseHabitacion).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TarifaFinalNoche).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ClienteResponsable).WithMany(p => p.ReservacionHabitaciones)
                .HasForeignKey(d => d.ClienteResponsableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__Clien__5AB9788F");

            entity.HasOne(d => d.Habitacion).WithMany(p => p.ReservacionHabitaciones)
                .HasForeignKey(d => d.HabitacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__Habit__58D1301D");

            entity.HasOne(d => d.Reservacion).WithMany(p => p.ReservacionHabitaciones)
                .HasForeignKey(d => d.ReservacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__Reser__57DD0BE4");

            entity.HasOne(d => d.TipoCliente).WithMany(p => p.ReservacionHabitaciones)
                .HasForeignKey(d => d.TipoClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__TipoC__59C55456");
        });

        modelBuilder.Entity<Reservacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservac__3214EC0788FC2BEA");

            entity.HasIndex(e => e.Codigo, "UQ__Reservac__06370DAC17DDB946").IsUnique();

            entity.Property(e => e.AnticipoRequerido).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacionUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.MontoDescuento).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MontoImpuestos).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MotivoCancelacion).HasMaxLength(300);
            entity.Property(e => e.Observaciones).HasMaxLength(500);
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Reservaciones)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__Clien__44CA3770");

            entity.HasOne(d => d.Descuento).WithMany(p => p.Reservaciones)
                .HasForeignKey(d => d.DescuentoId)
                .HasConstraintName("FK__Reservaci__Descu__47A6A41B");

            entity.HasOne(d => d.Estado).WithMany(p => p.Reservaciones)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__Estad__498EEC8D");

            entity.HasOne(d => d.Impuesto).WithMany(p => p.Reservaciones)
                .HasForeignKey(d => d.ImpuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__Impue__489AC854");

            entity.HasOne(d => d.Temporada).WithMany(p => p.Reservaciones)
                .HasForeignKey(d => d.TemporadaId)
                .HasConstraintName("FK__Reservaci__Tempo__46B27FE2");

            entity.HasOne(d => d.TipoCliente).WithMany(p => p.Reservaciones)
                .HasForeignKey(d => d.TipoClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__TipoC__45BE5BA9");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC079C2C22F1");

            entity.HasIndex(e => e.Nombre, "UQ__Roles__75E3EFCF2319CE90").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasMany(d => d.Permisos).WithMany(p => p.Rols)
                .UsingEntity<Dictionary<string, object>>(
                    "RolPermiso",
                    r => r.HasOne<Permiso>().WithMany()
                        .HasForeignKey("PermisoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RolPermis__Permi__778AC167"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RolPermis__RolId__76969D2E"),
                    j =>
                    {
                        j.HasKey("RolId", "PermisoId").HasName("PK__RolPermi__D04D0E83C5A237CC");
                        j.ToTable("RolPermisos");
                    });
        });

        modelBuilder.Entity<Temporada>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Temporad__3214EC07F463A3D4");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(60);
            entity.Property(e => e.PorcentajeAjuste).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.PorcentajeRecargoFinSemana).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TipoTemporada)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TiposCama>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TiposCam__3214EC076E0EFC63");

            entity.ToTable("TiposCama");

            entity.HasIndex(e => e.Nombre, "UQ__TiposCam__75E3EFCF790CEF20").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<TiposCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TiposCli__3214EC0797D3E3A6");

            entity.ToTable("TiposCliente");

            entity.HasIndex(e => e.Nombre, "UQ__TiposCli__75E3EFCFC627D34A").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.PorcentajeDescuentoDefault).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<TiposDocumento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TiposDoc__3214EC07C1F082FE");

            entity.ToTable("TiposDocumento");

            entity.HasIndex(e => e.Codigo, "UQ__TiposDoc__06370DAC9CA32D07").IsUnique();

            entity.HasIndex(e => e.Nombre, "UQ__TiposDoc__75E3EFCFB58577A6").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LongitudMaxima).HasDefaultValue(30);
            entity.Property(e => e.LongitudMinima).HasDefaultValue(5);
            entity.Property(e => e.Nombre).HasMaxLength(60);
        });

        modelBuilder.Entity<TiposHabitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TiposHab__3214EC079F561E06");

            entity.ToTable("TiposHabitacion");

            entity.HasIndex(e => e.Nombre, "UQ__TiposHab__75E3EFCF22534107").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CargoPersonaAdicional).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.PrecioBasePorNoche).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07128DD15D");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__6B0F5AE07B899160").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D105347C1FC636").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FechaCreacionUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.NombreCompleto).HasMaxLength(200);
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__Usuarios__Client__05D8E0BE");

            entity.HasMany(d => d.Rols).WithMany(p => p.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsuarioRo__RolId__09A971A2"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsuarioRo__Usuar__08B54D69"),
                    j =>
                    {
                        j.HasKey("UsuarioId", "RolId").HasName("PK__UsuarioR__24AFD797E97401E5");
                        j.ToTable("UsuarioRoles");
                    });
        });
        modelBuilder.HasSequence<int>("SeqReservacion");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
