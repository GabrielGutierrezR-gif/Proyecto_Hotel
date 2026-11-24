using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Temporada
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string TipoTemporada { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public decimal PorcentajeAjuste { get; set; }

    public decimal PorcentajeRecargoFinSemana { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Reservacione> Reservaciones { get; set; } = new List<Reservacione>();
}
