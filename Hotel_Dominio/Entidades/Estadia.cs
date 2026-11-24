using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Estadia
{
    public int Id { get; set; }

    public int ReservacionId { get; set; }

    public DateTime CheckInRealUtc { get; set; }

    public DateTime? CheckOutRealUtc { get; set; }

    public int UsuarioCheckInId { get; set; }

    public int? UsuarioCheckOutId { get; set; }

    public string? CodigoAcceso { get; set; }

    public virtual Reservacione Reservacion { get; set; } = null!;

    public virtual Usuario UsuarioCheckIn { get; set; } = null!;

    public virtual Usuario? UsuarioCheckOut { get; set; }
}
