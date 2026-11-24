using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class CargosAdicionale
{
    public int Id { get; set; }

    public int ReservacionId { get; set; }

    public string Tipo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Subtotal { get; set; }

    public int? ImpuestoId { get; set; }

    public decimal MontoImpuesto { get; set; }

    public decimal Total { get; set; }

    public string? Justificacion { get; set; }

    public int UsuarioId { get; set; }

    public DateTime FechaUtc { get; set; }

    public virtual Impuesto? Impuesto { get; set; }

    public virtual Reservacione Reservacion { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
