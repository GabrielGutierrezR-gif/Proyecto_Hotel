using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class TiposCama
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int CapacidadPersonas { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<HabitacionCama> HabitacionCamas { get; set; } = new List<HabitacionCama>();
}
