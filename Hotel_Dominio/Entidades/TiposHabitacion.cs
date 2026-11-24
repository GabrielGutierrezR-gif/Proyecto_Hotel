using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class TiposHabitacion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int CapacidadBaseAdultos { get; set; }

    public int CapacidadMaximaAdultos { get; set; }

    public int CapacidadMaximaNinos { get; set; }

    public decimal PrecioBasePorNoche { get; set; }

    public decimal CargoPersonaAdicional { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Habitacione> Habitaciones { get; set; } = new List<Habitacione>();
}
