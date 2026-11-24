using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class HuespedesReservacion
{
    public int Id { get; set; }

    public int ReservacionId { get; set; }

    public int HabitacionId { get; set; }

    public int ClienteTitularReferenteId { get; set; }

    public int TipoDocumentoId { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int PoliticaEdadId { get; set; }

    public decimal TarifaAplicadaHuesped { get; set; }

    public virtual Cliente ClienteTitularReferente { get; set; } = null!;

    public virtual Habitacione Habitacion { get; set; } = null!;

    public virtual PoliticasEdad PoliticaEdad { get; set; } = null!;

    public virtual Reservacione Reservacion { get; set; } = null!;

    public virtual TiposDocumento TipoDocumento { get; set; } = null!;
}
