using System.ComponentModel.DataAnnotations;

namespace Hotel_Dominio.DTOs
{
    public class RegistroRequestDto
    {
        [Required] public string NombreUsuario { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)] public string Password { get; set; } = string.Empty;
        [Required] public string NombreCompleto { get; set; } = string.Empty;
        public string Rol { get; set; } = "Admin"; // Por defecto Admin para pruebas iniciales
    }

    public class LoginRequestDto
    {
        [Required] public string UsuarioOEmail { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
    }

    public class RefreshRequestDto
    {
        [Required] public string RefreshToken { get; set; } = string.Empty;
    }

    public class AuthResponseDto
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiracionAccessToken { get; set; }
    }
}