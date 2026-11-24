using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class CategoriasPlatillo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<Platillo> Platillos { get; set; } = new List<Platillo>();
}
