using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Permiso
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public virtual ICollection<Role> Rols { get; set; } = new List<Role>();
}
