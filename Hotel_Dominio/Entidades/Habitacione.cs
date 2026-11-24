using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Habitacione
{
    public int Id { get; set; }

    public string Numero { get; set; } = null!;

    public int Piso { get; set; }

    public int TipoHabitacionId { get; set; }

    public int EstadoId { get; set; }

    public decimal PrecioPorNoche { get; set; }

    public string? ImagenUrl { get; set; }

    public bool Activo { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<HabitacionCama> HabitacionCamas { get; set; } = new List<HabitacionCama>();

    public virtual ICollection<HistorialEstadoHabitacion> HistorialEstadoHabitacions { get; set; } = new List<HistorialEstadoHabitacion>();

    public virtual ICollection<HuespedesReservacion> HuespedesReservacions { get; set; } = new List<HuespedesReservacion>();

    public virtual ICollection<PedidosRestaurante> PedidosRestaurantes { get; set; } = new List<PedidosRestaurante>();

    public virtual ICollection<ReservacionHabitacione> ReservacionHabitaciones { get; set; } = new List<ReservacionHabitacione>();

    public virtual TiposHabitacion TipoHabitacion { get; set; } = null!;

    public virtual ICollection<Amenidade> Amenidads { get; set; } = new List<Amenidade>();
}
