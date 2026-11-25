using Hotel_Dominio.DTOs;
using Hotel_Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthLN _authLN;

        public AuthController(IAuthLN authLN)
        {
            _authLN = authLN;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroRequestDto request)
        {
            var resultado = await _authLN.RegistrarAsync(request);
            return resultado.Exito ? Ok(resultado) : BadRequest(resultado);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var resultado = await _authLN.LoginAsync(request);
            return resultado.Exito ? Ok(resultado) : Unauthorized(resultado);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto request)
        {
            var resultado = await _authLN.RefrescarTokenAsync(request.RefreshToken);
            return resultado.Exito ? Ok(resultado) : Unauthorized(resultado);
        }

        [Authorize]
        [HttpGet("perfil")]
        public IActionResult Perfil()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(new { Exito = true, Mensaje = "Token válido", Datos = claims });
        }
    }
}