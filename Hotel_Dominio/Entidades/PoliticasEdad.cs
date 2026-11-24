using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class PoliticasEdad
{
    public int Id { get; set; }

    public string Categoria { get; set; } = null!;

    public int EdadMinima { get; set; }

    public int EdadMaxima { get; set; }

    public decimal PorcentajeTarifa { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<HuespedesReservacion> HuespedesReservacions { get; set; } = new List<HuespedesReservacion>();
}
