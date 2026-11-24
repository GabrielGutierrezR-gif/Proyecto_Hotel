using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class HabitacionCama
{
    public int HabitacionId { get; set; }

    public int TipoCamaId { get; set; }

    public int Cantidad { get; set; }

    public virtual Habitacione Habitacion { get; set; } = null!;

    public virtual TiposCama TipoCama { get; set; } = null!;
}
