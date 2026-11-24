using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Reservacione
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public int ClienteId { get; set; }

    public int TipoClienteId { get; set; }

    public int? TemporadaId { get; set; }

    public int? DescuentoId { get; set; }

    public int ImpuestoId { get; set; }

    public int EstadoId { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public int CantidadAdultos { get; set; }

    public int CantidadNinos { get; set; }

    public decimal Subtotal { get; set; }

    public decimal MontoDescuento { get; set; }

    public decimal MontoImpuestos { get; set; }

    public decimal Total { get; set; }

    public decimal AnticipoRequerido { get; set; }

    public string? Observaciones { get; set; }

    public string? MotivoCancelacion { get; set; }

    public DateTime FechaCreacionUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<CargosAdicionale> CargosAdicionales { get; set; } = new List<CargosAdicionale>();

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Descuento? Descuento { get; set; }

    public virtual Estadia? Estadia { get; set; }

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<HistorialReservacion> HistorialReservacions { get; set; } = new List<HistorialReservacion>();

    public virtual ICollection<HuespedesReservacion> HuespedesReservacions { get; set; } = new List<HuespedesReservacion>();

    public virtual Impuesto Impuesto { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<PedidosRestaurante> PedidosRestaurantes { get; set; } = new List<PedidosRestaurante>();

    public virtual ICollection<ReservacionHabitacione> ReservacionHabitaciones { get; set; } = new List<ReservacionHabitacione>();

    public virtual Temporada? Temporada { get; set; }

    public virtual TiposCliente TipoCliente { get; set; } = null!;
}
