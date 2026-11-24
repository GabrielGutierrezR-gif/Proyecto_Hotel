using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class ReservacionHabitacione
{
    public int Id { get; set; }

    public int ReservacionId { get; set; }

    public int HabitacionId { get; set; }

    public int TipoClienteId { get; set; }

    public int ClienteResponsableId { get; set; }

    public decimal TarifaBaseHabitacion { get; set; }

    public decimal AjusteTemporada { get; set; }

    public decimal CargoHuespedesAdicionales { get; set; }

    public decimal TarifaFinalNoche { get; set; }

    public virtual Cliente ClienteResponsable { get; set; } = null!;

    public virtual Habitacione Habitacion { get; set; } = null!;

    public virtual Reservacione Reservacion { get; set; } = null!;

    public virtual TiposCliente TipoCliente { get; set; } = null!;
}
