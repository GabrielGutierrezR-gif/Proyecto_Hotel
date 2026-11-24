using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Auditorium
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public string Accion { get; set; } = null!;

    public string Entidad { get; set; } = null!;

    public int EntidadId { get; set; }

    public DateTime FechaUtc { get; set; }

    public string? Datos { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
