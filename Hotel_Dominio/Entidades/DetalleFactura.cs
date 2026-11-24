using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class DetalleFactura
{
    public int Id { get; set; }

    public int FacturaId { get; set; }

    public string Concepto { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal MontoDescuento { get; set; }

    public decimal MontoImpuesto { get; set; }

    public decimal TotalLinea { get; set; }

    public virtual Factura Factura { get; set; } = null!;
}
