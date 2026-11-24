using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Estado
{
    public int Id { get; set; }

    public string Entidad { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Habitacione> Habitaciones { get; set; } = new List<Habitacione>();

    public virtual ICollection<HistorialEstadoHabitacion> HistorialEstadoHabitacionEstadoAnteriors { get; set; } = new List<HistorialEstadoHabitacion>();

    public virtual ICollection<HistorialEstadoHabitacion> HistorialEstadoHabitacionEstadoNuevos { get; set; } = new List<HistorialEstadoHabitacion>();

    public virtual ICollection<HistorialReservacion> HistorialReservacionEstadoAnteriors { get; set; } = new List<HistorialReservacion>();

    public virtual ICollection<HistorialReservacion> HistorialReservacionEstadoNuevos { get; set; } = new List<HistorialReservacion>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<PedidosRestaurante> PedidosRestaurantes { get; set; } = new List<PedidosRestaurante>();

    public virtual ICollection<Reservacione> Reservaciones { get; set; } = new List<Reservacione>();
}
