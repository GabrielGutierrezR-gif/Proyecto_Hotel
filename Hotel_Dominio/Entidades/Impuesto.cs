using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Impuesto
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public decimal Porcentaje { get; set; }

    public bool AplicaHospedaje { get; set; }

    public bool AplicaRestaurante { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<CargosAdicionale> CargosAdicionales { get; set; } = new List<CargosAdicionale>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<PedidosRestaurante> PedidosRestaurantes { get; set; } = new List<PedidosRestaurante>();

    public virtual ICollection<Reservacione> Reservaciones { get; set; } = new List<Reservacione>();
}
