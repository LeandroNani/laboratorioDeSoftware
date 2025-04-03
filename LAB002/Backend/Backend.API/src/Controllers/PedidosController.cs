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

        // ------------------------------------------------------------
        // 1) CADASTRO DE SOLICITAÇÃO DE ALUGUEL (Clientes podem fazer)
        // POST: /api/pedidos
        [HttpPost]
        [Authorize(Roles="CLIENTE")]
        public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoRequest request)
        {
            // ID do cliente vem no request

            var cliente = await _context.Clientes.FindAsync(request.ClienteId);
            if (cliente == null)
                return BadRequest("Cliente inválido.");

            var automovel = await _context.Automoveis
                .FirstOrDefaultAsync(a => a.Id == request.AutomovelId);

            if (automovel == null)
                return BadRequest("Automóvel inválido.");

            // Cria Pedido
            var pedido = new Pedido
            {
                ClienteId = request.ClienteId,
                AutomovelId = request.AutomovelId,
                AgenteId = automovel.AgenteId,  // extrai do Automovel
                Status = "pendente"
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Pedido criado com Id {pedido.Id}", pedidoId = pedido.Id });
        }

        // ------------------------------------------------------------
        // 2) EDIÇÃO DA SOLICITAÇÃO DE ALUGUEL (status != "aprovado")
        // PUT: /api/pedidos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles="CLIENTE")]
        public async Task<IActionResult> EditarPedido(int id, [FromBody] EditarPedidoRequest request)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Automovel)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound("Pedido não encontrado.");

            // Verifica se ainda está pendente ou negado (exemplo)
            if (pedido.Status == "aprovado")
                return BadRequest($"Não é possível editar um pedido {pedido.Status}.");

            // Se quiser, garanta que o ID do cliente seja o mesmo do JWT
            // var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // if (pedido.ClienteId != userId) return Forbid();

            var novoAuto = await _context.Automoveis.FindAsync(request.NovoAutomovelId);
            if (novoAuto == null)
                return BadRequest("Novo automóvel inválido.");

            pedido.AutomovelId = novoAuto.Id;
            pedido.AgenteId = novoAuto.AgenteId;
            // mantém status inalterado (ex.: "pendente" ou "negado")

            await _context.SaveChangesAsync();
            return Ok($"Pedido {pedido.Id} atualizado com automóvel {novoAuto.Id}.");
        }

        // ------------------------------------------------------------
        // 3) LISTAR SOLICITAÇÕES DE AUTOMÓVEL QUE O CLIENTE FEZ
        // GET: /api/pedidos/minhas-solicitacoes?clienteId=XYZ
        [HttpGet("minhas-solicitacoes")]
        [Authorize(Roles="CLIENTE")]
        public async Task<IActionResult> ListarSolicitacoes([FromQuery] int clienteId)
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Automovel)
                .Include(p => p.Agente)
                .Where(p => p.ClienteId == clienteId)
                .ToListAsync();

            return Ok(pedidos);
        }

        // ------------------------------------------------------------
        // 4) CANCELAMENTO DE PEDIDO (status = "cancelado")
        // DELETE: /api/pedidos/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles="CLIENTE")]
        public async Task<IActionResult> CancelarPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                return NotFound("Pedido não encontrado.");

            // Se quiser, confira se o cliente é o dono do pedido
            // var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // if (pedido.ClienteId != userId) return Forbid();

            pedido.Status = "cancelado";
            await _context.SaveChangesAsync();

            return Ok($"Pedido {pedido.Id} cancelado com sucesso.");
        }
    }

    // DTOS
    public class CriarPedidoRequest
    {
        public int ClienteId { get; set; }
        public int AutomovelId { get; set; }
    }

    public class EditarPedidoRequest
    {
        public int NovoAutomovelId { get; set; }
    }
}
