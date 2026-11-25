using Hotel_AccesoDatos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Hotel_AccesoDatos.Contexto;
using Hotel_Dominio.DTOs;
using Hotel_Dominio.Interfaces;
using Hotel_Utilitarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Hotel_LogicaNegocio.Implementaciones
{
    public class AuthLN : IAuthLN
    {
        private readonly HotelDbContext _context;
        private readonly IConfiguration _config;

        public AuthLN(HotelDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<Respuesta<AuthResponseDto>> RegistrarAsync(RegistroRequestDto request)
        {
            var existe = await _context.Usuarios
                .AnyAsync(u => u.NombreUsuario == request.NombreUsuario || u.Email == request.Email);

            if (existe)
                return Respuesta<AuthResponseDto>.Error("El nombre de usuario o correo ya está registrado.");

            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == request.Rol)
                      ?? await _context.Roles.FirstAsync();

            var nuevoUsuario = new Usuario
            {
                NombreUsuario = request.NombreUsuario.Trim(),
                Email = request.Email.Trim().ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                NombreCompleto = request.NombreCompleto.Trim(),
                Activo = true,
                FechaCreacionUtc = DateTime.UtcNow
            };

            nuevoUsuario.Rols.Add(rol);
            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return await GenerarRespuestaAutenticacionAsync(nuevoUsuario, new List<string> { rol.Nombre });
        }

        public async Task<Respuesta<AuthResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var identificador = request.UsuarioOEmail.Trim().ToLower();
            var usuario = await _context.Usuarios
                .Include(u => u.Rols)
                .FirstOrDefaultAsync(u =>
                    (u.NombreUsuario.ToLower() == identificador || u.Email.ToLower() == identificador) && u.Activo);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
                return Respuesta<AuthResponseDto>.Error("Credenciales inválidas.");

            var roles = usuario.Rols.Select(r => r.Nombre).ToList();
            return await GenerarRespuestaAutenticacionAsync(usuario, roles);
        }

        public async Task<Respuesta<AuthResponseDto>> RefrescarTokenAsync(string refreshTokenPlano)
        {
            var tokenHash = HashSha256(refreshTokenPlano);
            var tokenDb = await _context.RefreshTokens
                .Include(rt => rt.Usuario)
                .ThenInclude(u => u.Rols)
                .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash);

            if (tokenDb == null || tokenDb.FechaRevocacionUtc != null || tokenDb.FechaExpiracionUtc <= DateTime.UtcNow)
                return Respuesta<AuthResponseDto>.Error("Refresh token inválido o expirado.");

            var roles = tokenDb.Usuario.Rols.Select(r => r.Nombre).ToList();
            var respuesta = await GenerarRespuestaAutenticacionAsync(tokenDb.Usuario, roles);

            tokenDb.FechaRevocacionUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return respuesta;
        }

        private async Task<Respuesta<AuthResponseDto>> GenerarRespuestaAutenticacionAsync(Usuario usuario, List<string> roles)
        {
            var minutosExp = int.Parse(_config["Jwt:AccessTokenMinutes"] ?? "30");
            var diasRefresh = int.Parse(_config["Jwt:RefreshTokenDays"] ?? "7");
            var expiracion = DateTime.UtcNow.AddMinutes(minutosExp);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.NombreUsuario),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim("nombreCompleto", usuario.NombreCompleto)
            };

            foreach (var rol in roles)
                claims.Add(new Claim(ClaimTypes.Role, rol));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expiracion,
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            var refreshTokenPlano = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            _context.RefreshTokens.Add(new RefreshToken
            {
                UsuarioId = usuario.Id,
                TokenHash = HashSha256(refreshTokenPlano),
                FechaCreacionUtc = DateTime.UtcNow,
                FechaExpiracionUtc = DateTime.UtcNow.AddDays(diasRefresh)
            });

            await _context.SaveChangesAsync();

            return Respuesta<AuthResponseDto>.Ok(new AuthResponseDto
            {
                UsuarioId = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                Roles = roles,
                AccessToken = accessToken,
                RefreshToken = refreshTokenPlano,
                ExpiracionAccessToken = expiracion
            }, "Autenticación exitosa.");
        }

        private static string HashSha256(string input)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }
    }
}