using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class Pago
{
    public int Id { get; set; }

    public int ReservacionId { get; set; }

    public int EstadoId { get; set; }

    public string Tipo { get; set; } = null!;

    public decimal Monto { get; set; }

    public string MetodoPago { get; set; } = null!;

    public string? Referencia { get; set; }

    public string? UltimosCuatro { get; set; }

    public int? PagoOrigenId { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime FechaPagoUtc { get; set; }

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<Pago> InversePagoOrigen { get; set; } = new List<Pago>();

    public virtual Pago? PagoOrigen { get; set; }

    public virtual Reservacione Reservacion { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
