using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacionUtc { get; set; }

    public int? ClienteId { get; set; }

    public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();

    public virtual ICollection<CargosAdicionale> CargosAdicionales { get; set; } = new List<CargosAdicionale>();

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<Estadia> EstadiaUsuarioCheckIns { get; set; } = new List<Estadia>();

    public virtual ICollection<Estadia> EstadiaUsuarioCheckOuts { get; set; } = new List<Estadia>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<HistorialEstadoHabitacion> HistorialEstadoHabitacions { get; set; } = new List<HistorialEstadoHabitacion>();

    public virtual ICollection<HistorialReservacion> HistorialReservacions { get; set; } = new List<HistorialReservacion>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<Role> Rols { get; set; } = new List<Role>();
}
