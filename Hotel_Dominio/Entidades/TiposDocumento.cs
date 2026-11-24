using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class TiposDocumento
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int LongitudMinima { get; set; }

    public int LongitudMaxima { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<HuespedesReservacion> HuespedesReservacions { get; set; } = new List<HuespedesReservacion>();
}
