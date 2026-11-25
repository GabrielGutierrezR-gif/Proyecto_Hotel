using Hotel_Dominio.DTOs;
using Hotel_Utilitarios;

namespace Hotel_Dominio.Interfaces
{
    public interface IAuthLN
    {
        Task<Respuesta<AuthResponseDto>> RegistrarAsync(RegistroRequestDto request);
        Task<Respuesta<AuthResponseDto>> LoginAsync(LoginRequestDto request);
        Task<Respuesta<AuthResponseDto>> RefrescarTokenAsync(string refreshToken);
    }
}