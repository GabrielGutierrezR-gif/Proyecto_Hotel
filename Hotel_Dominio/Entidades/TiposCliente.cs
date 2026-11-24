using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class TiposCliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal PorcentajeDescuentoDefault { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<ReservacionHabitacione> ReservacionHabitaciones { get; set; } = new List<ReservacionHabitacione>();

    public virtual ICollection<Reservacione> Reservaciones { get; set; } = new List<Reservacione>();
}
