using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Descuento
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string TipoValor { get; set; } = null!;

    public decimal Valor { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public bool AplicaHospedaje { get; set; }

    public bool AplicaRestaurante { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<PedidosRestaurante> PedidosRestaurantes { get; set; } = new List<PedidosRestaurante>();

    public virtual ICollection<Reservacione> Reservaciones { get; set; } = new List<Reservacione>();
}
