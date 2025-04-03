using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Backend.API.Data;
using Backend.API.Model;
using System.Security.Claims;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutomoveisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AutomoveisController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um automóvel (somente ADMIN).
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CriarAutomovel([FromBody] CriarAutomovelRequest request)
        {
            var automovel = new Automovel
            {
                Marca = request.Marca,
                Modelo = request.Modelo,
                Ano = request.Ano,
                Placa = request.Placa,
                FotoUrl = request.FotoUrl,
                AgenteId = request.AgenteId
            };

            _context.Automoveis.Add(automovel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Automóvel cadastrado com sucesso", automovelId = automovel.Id });
        }

        /// <summary>
        /// Lista de automóveis visível para clientes.
        /// </summary>
        [HttpGet("disponiveis")]
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> ListarParaClientes()
        {
            var lista = await _context.Automoveis
                .Select(a => new
                {
                    a.Id,
                    a.Marca,
                    a.Modelo
                }).ToListAsync();

            return Ok(lista);
        }

        /// <summary>
        /// Lista de automóveis do agente logado.
        /// </summary>
        [HttpGet("meus-automoveis")]
        [Authorize(Roles = "AGENTE")]
        public async Task<IActionResult> ListarParaAgente()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int agenteId))
                return Unauthorized("Agente não identificado.");

            var lista = await _context.Automoveis
                .Where(a => a.AgenteId == agenteId)
                .ToListAsync();

            return Ok(lista);
        }
    }

    public class CriarAutomovelRequest
    {
        public required string Marca { get; set; }
        public required string Modelo { get; set; }
        public required int Ano { get; set; }
        public required string Placa { get; set; }
        public string? FotoUrl { get; set; }
        public required int AgenteId { get; set; }
    }
}
