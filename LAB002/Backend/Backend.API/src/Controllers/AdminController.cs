
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.API.Data;
using Backend.API.Model;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo agente no sistema (somente Admin).
        /// </summary>
        /// <param name="request">Dados do agente a ser criado</param>
        /// <returns>Mensagem de sucesso ou erro</returns>
        /// <response code="200">Agente criado com sucesso</response>
        /// <response code="400">Já existe um agente com esse e-mail</response>
        [HttpPost("criar-agente")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CriarAgente([FromBody] CriarAgenteRequest request)
        {
            bool existe = await _context.Agentes.AnyAsync(a => a.Email == request.Email);
            if (existe)
                return BadRequest("Já existe um agente com esse e-mail.");

            var agente = new Agente
            {
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                CNPJ = request.Cnpj,
                Endereco = request.Endereco,
                QuantidadeCarros = request.QuantidadeCarros,
                Role = Role.ADMIN  
            };

            _context.Agentes.Add(agente);
            await _context.SaveChangesAsync();

            return Ok($"Agente criado com ID {agente.Id}");
        }
    }

    public class CriarAgenteRequest
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string Cnpj { get; set; }
        public string? Endereco { get; set; }
        public required int QuantidadeCarros { get; set; }
    }
}
