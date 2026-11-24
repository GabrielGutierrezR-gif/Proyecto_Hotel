using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Amenidade
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<Habitacione> Habitacions { get; set; } = new List<Habitacione>();
}
