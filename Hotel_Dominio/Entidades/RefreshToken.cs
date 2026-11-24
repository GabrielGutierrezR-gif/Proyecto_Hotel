using System;
using System.Collections.Generic;

namespace Hotel_AccesoDatos;

public partial class RefreshToken
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string TokenHash { get; set; } = null!;

    public DateTime FechaCreacionUtc { get; set; }

    public DateTime FechaExpiracionUtc { get; set; }

    public DateTime? FechaRevocacionUtc { get; set; }

    public int? TokenReemplazoId { get; set; }

    public virtual ICollection<RefreshToken> InverseTokenReemplazo { get; set; } = new List<RefreshToken>();

    public virtual RefreshToken? TokenReemplazo { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
