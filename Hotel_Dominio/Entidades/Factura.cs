using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Factura
{
    public int Id { get; set; }

    public string NumeroFactura { get; set; } = null!;

    public int ReservacionId { get; set; }

    public int ClienteId { get; set; }

    public int? DescuentoId { get; set; }

    public int ImpuestoId { get; set; }

    public decimal Subtotal { get; set; }

    public decimal MontoDescuento { get; set; }

    public decimal MontoImpuestos { get; set; }

    public decimal Total { get; set; }

    public int UsuarioId { get; set; }

    public DateTime FechaEmisionUtc { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Descuento? Descuento { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Impuesto Impuesto { get; set; } = null!;

    public virtual Reservacione Reservacion { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
