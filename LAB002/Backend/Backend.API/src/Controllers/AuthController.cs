using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.API.Data;
using Backend.API.Model;
using BCrypt.Net;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            Usuario? usuario = request.TipoUsuario.ToLower() switch
            {
                "admin" => await _context.Admins.FirstOrDefaultAsync(u => u.Email == request.Email),
                "cliente" => await _context.Clientes.FirstOrDefaultAsync(u => u.Email == request.Email),
                "agente" => await _context.Agentes.FirstOrDefaultAsync(u => u.Email == request.Email),
                _ => null
            };

            bool senhaCorreta = BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash);

            if (!senhaCorreta)
            {
                return Unauthorized("Senha incorreta.");
            }

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
                return Unauthorized("Credenciais inválidas.");

            var token = GerarToken(usuario);
            return Ok(new LoginResponse
            {
                Token = token,
                TipoUsuario = usuario.Role.ToString(),
                UsuarioId = usuario.Id
            });
        }

        private string GerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string TipoUsuario { get; set; }  // "admin", "cliente", "agente"
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string TipoUsuario { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
    }
}
