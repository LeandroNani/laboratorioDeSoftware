using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Backend.API.Data;
using Backend.API.Model;

namespace Backend.API.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar Contratos de aluguel.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ContratosController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor do ContratosController.
        /// </summary>
        /// <param name="context">Contexto de acesso ao banco de dados</param>
        public ContratosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista os contratos de acordo com o papel do usuário:
        /// - CLIENTE: vê somente seus próprios contratos.
        /// - AGENTE: vê somente os contratos vinculados a ele.
        /// (Opcional) Se desejar, ADMIN pode ver todos os contratos.
        /// </summary>
        /// <remarks>
        /// É necessário fornecer um token JWT válido no cabeçalho:
        /// <code>Authorization: Bearer [seu_token]</code>
        /// </remarks>
        /// <returns>Retorna uma lista de Contratos, ou mensagens de erro/forbidden/unauthorized.</returns>
        /// <response code="200">Retorna a lista de Contratos do usuário logado</response>
        /// <response code="401">Usuário não autorizado (token inválido ou ausente)</response>
        /// <response code="403">Usuário não possui permissão</response>
        [HttpGet("meus-contratos")]
        [Authorize(Roles = "CLIENTE,AGENTE")] 
        public async Task<IActionResult> ListarMeusContratos()
        {
            // Obtém o ID e o papel do usuário através do token JWT
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (!int.TryParse(userIdStr, out int usuarioId))
                return Unauthorized("Não foi possível identificar o usuário.");

            // Filtra de acordo com o role do usuário
            if (role == "CLIENTE")
            {
                // Somente contratos cujo ClienteId = usuarioId
                var contratosCliente = await _context.Contratos
                    .Where(c => c.ClienteId == usuarioId)
                    .ToListAsync();
                return Ok(contratosCliente);
            }
            else if (role == "AGENTE")
            {
                // Somente contratos cujo AgenteId = usuarioId
                var contratosAgente = await _context.Contratos
                    .Where(c => c.AgenteId == usuarioId)
                    .ToListAsync();
                return Ok(contratosAgente);
            }

            // Se quiser que ADMIN veja todos, inclua a role ADMIN e esse bloco:
            // else if (role == "ADMIN")
            // {
            //     var todosContratos = await _context.Contratos.ToListAsync();
            //     return Ok(todosContratos);
            // }

            return Forbid("Acesso não permitido para este tipo de usuário.");
        }
    }
}
