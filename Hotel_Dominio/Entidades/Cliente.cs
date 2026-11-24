using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Cliente
{
    public int Id { get; set; }

    public int TipoDocumentoId { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public int TipoClienteId { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Nacionalidad { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Preferencias { get; set; }

    public DateTime FechaRegistroUtc { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<HuespedesReservacion> HuespedesReservacions { get; set; } = new List<HuespedesReservacion>();

    public virtual ICollection<PedidosRestaurante> PedidosRestaurantes { get; set; } = new List<PedidosRestaurante>();

    public virtual ICollection<ReservacionHabitacione> ReservacionHabitaciones { get; set; } = new List<ReservacionHabitacione>();

    public virtual ICollection<Reservacione> Reservaciones { get; set; } = new List<Reservacione>();

    public virtual TiposCliente TipoCliente { get; set; } = null!;

    public virtual TiposDocumento TipoDocumento { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
