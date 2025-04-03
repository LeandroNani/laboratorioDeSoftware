using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Backend.API.Data;
using Backend.API.Model;

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo pedido de aluguel.
        /// </summary>
        /// <param name="request">Dados do pedido</param>
        /// <returns>Mensagem de sucesso e ID do pedido</returns>
        [HttpPost]
        [Authorize(Roles="CLIENTE")]
        public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoRequest request)
        {
            // Verifica se o cliente existe
            var cliente = await _context.Clientes.FindAsync(request.ClienteId);
            if (cliente == null)
                return BadRequest("Cliente inválido.");

            // Verifica se automovel existe
            var automovel = await _context.Automoveis.FindAsync(request.AutomovelId);
            if (automovel == null)
                return BadRequest("Automóvel inválido.");

            // Pega o agente do automovel
            int agenteId = automovel.AgenteId ?? 0;  // Se automovel.AgenteId for int?, use ?? 0
            var agente = await _context.Agentes.FindAsync(agenteId);

            // Cria Pedido
            var pedido = new Pedido
            {
                ContratanteId = request.ClienteId,
                Contratante = cliente,

                AutomovelId = automovel.Id,
                Automovel = automovel,

                AgenteDesignadoId = agenteId,
                AgenteDesignado = agente,

                Duracao = request.Duracao,
                TipoContrato = request.TipoContrato ?? "credito",
                Status = "pendente"
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Pedido criado com Id {pedido.Id}", pedidoId = pedido.Id });
        }

        /// <summary>
        /// Edita um pedido, se não estiver aprovado.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <param name="request">Dados para edição</param>
        /// <returns>Mensagem de sucesso</returns>
        [HttpPut("{id}")]
        [Authorize(Roles="CLIENTE")]
        public async Task<IActionResult> EditarPedido(int id, [FromBody] EditarPedidoRequest request)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Automovel)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound("Pedido não encontrado.");

            // Se já aprovado, não pode editar
            if (pedido.Status == "aprovado")
                return BadRequest($"Não é possível editar um pedido {pedido.Status}.");

            // Se quiser, verifique se o autor é o mesmo cliente:
            // var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // if (pedido.ContratanteId != userId) return Forbid();

            var novoAuto = await _context.Automoveis.FindAsync(request.NovoAutomovelId);
            if (novoAuto == null)
                return BadRequest("Novo automóvel inválido.");

            // Ajusta o automóvel e agente designado
            pedido.AutomovelId = novoAuto.Id;
            pedido.Automovel = novoAuto;

            int novoAgenteId = novoAuto.AgenteId ?? 0;
            var novoAgente = await _context.Agentes.FindAsync(novoAgenteId);

            pedido.AgenteDesignadoId = novoAgenteId;
            pedido.AgenteDesignado = novoAgente;

            // Mantém status = pendente ou negado
            await _context.SaveChangesAsync();
            return Ok($"Pedido {pedido.Id} atualizado com automóvel {novoAuto.Id}.");
        }

        /// <summary>
        /// Lista todos os pedidos feitos por um cliente.
        /// </summary>
        /// <param name="clienteId">ID do cliente</param>
        /// <returns>Lista de pedidos</returns>
        [HttpGet("minhas-solicitacoes")]
        [Authorize(Roles="CLIENTE")]
        public async Task<IActionResult> ListarSolicitacoes([FromQuery] int clienteId)
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Automovel)
                .Include(p => p.AgenteDesignado)
                .Where(p => p.ContratanteId == clienteId)
                .ToListAsync();

            return Ok(pedidos);
        }

        /// <summary>
        /// Cancela um pedido existente.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <returns>Mensagem de sucesso</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles="CLIENTE")]
        public async Task<IActionResult> CancelarPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                return NotFound("Pedido não encontrado.");

            // Verifique se é dono do pedido
            // var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // if (pedido.ContratanteId != userId) return Forbid();

            pedido.Status = "cancelado";
            await _context.SaveChangesAsync();

            return Ok($"Pedido {pedido.Id} cancelado com sucesso.");
        }
    }

    // DTOs
    public class CriarPedidoRequest
    {
        public int ClienteId { get; set; }
        public int AutomovelId { get; set; }
        public int Duracao { get; set; }
        public string? TipoContrato { get; set; }
    }

    public class EditarPedidoRequest
    {
        public int NovoAutomovelId { get; set; }
    }
}
