using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class PedidosRestaurante
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public int? ReservacionId { get; set; }

    public int? HabitacionId { get; set; }

    public int EstadoId { get; set; }

    public int? DescuentoId { get; set; }

    public int ImpuestoId { get; set; }

    public string Modalidad { get; set; } = null!;

    public bool CargoAhabitacion { get; set; }

    public DateTime FechaPedidoUtc { get; set; }

    public DateTime? FechaHoraDeseadaUtc { get; set; }

    public string? Instrucciones { get; set; }

    public decimal Subtotal { get; set; }

    public decimal MontoDescuento { get; set; }

    public decimal Recargo { get; set; }

    public decimal MontoImpuestos { get; set; }

    public decimal Total { get; set; }

    public DateTime? FechaListoUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Descuento? Descuento { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Estado Estado { get; set; } = null!;

    public virtual Habitacione? Habitacion { get; set; }

    public virtual Impuesto Impuesto { get; set; } = null!;

    public virtual Reservacione? Reservacion { get; set; }
}
