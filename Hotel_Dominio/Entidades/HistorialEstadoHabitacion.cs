using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class HistorialEstadoHabitacion
{
    public int Id { get; set; }

    public int HabitacionId { get; set; }

    public int EstadoAnteriorId { get; set; }

    public int EstadoNuevoId { get; set; }

    public int? UsuarioId { get; set; }

    public string? Motivo { get; set; }

    public DateTime FechaUtc { get; set; }

    public virtual Estado EstadoAnterior { get; set; } = null!;

    public virtual Estado EstadoNuevo { get; set; } = null!;

    public virtual Habitacione Habitacion { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
