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
    [Authorize(Roles="AGENTE")]
    public class AgenteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgenteController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os pedidos pendentes atribuídos ao agente logado.
        /// </summary>
        /// <returns>Lista de pedidos pendentes</returns>
        // GET /api/agente/pedidos-pendentes
        [HttpGet("pedidos-pendentes")]
        public async Task<IActionResult> ListarPedidosPendentes()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int agenteId))
                return Unauthorized("Agente não identificado.");

            // Busca todos os pedidos "pendente" cujo AgenteDesignadoId == agenteId
            var pedidosPendentes = await _context.Pedidos
                .Where(p => p.Status == "pendente" && p.AgenteDesignadoId == agenteId)
                .Include(p => p.Contratante)
                .Include(p => p.Automovel)
                .ToListAsync();

            var retorno = pedidosPendentes.Select(p => new PedidoPendenteDTO
            {
                PedidoId = p.Id,
                NomeCliente = p.Contratante != null ? p.Contratante.Nome : "Desconhecido",
                CpfCliente = p.Contratante != null ? p.Contratante.CPF : "N/A",
                MarcaCarro = p.Automovel != null ? p.Automovel.Marca : "N/A",
                ModeloCarro = p.Automovel != null ? p.Automovel.Modelo : "N/A",
                FotoCarro = p.Automovel != null ? p.Automovel.FotoUrl : null
            }).ToList();

            return Ok(retorno);
        }

        /// <summary>
        /// Detalha um pedido específico.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <returns>Detalhes do pedido</returns>
        /// <response code="200">Pedido encontrado</response>
        /// <response code="404">Pedido não encontrado</response>
        [HttpGet("pedidos/{id}")]
        public async Task<IActionResult> DetalharPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Contratante)
                .ThenInclude(c => c.Rendimentos)
                .Include(p => p.Automovel)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound("Pedido não encontrado.");

            // Se quiser, verifique se p.AgenteDesignadoId == userId

            var dto = new DetalhePedidoDTO
            {
                PedidoId = pedido.Id,
                Status = pedido.Status,
                NomeCliente = pedido.Contratante?.Nome,
                CpfCliente = pedido.Contratante?.CPF,
                ProfissaoCliente = pedido.Contratante?.Profissao,
                Rendimentos = pedido.Contratante?.Rendimentos?
                    .Select(r => new RendimentoDTO { Valor = r.Valor, Fonte = r.Fonte })
                    .ToList() ?? new List<RendimentoDTO>(),

                AnoCarro = pedido.Automovel?.Ano,
                MarcaCarro = pedido.Automovel?.Marca,
                ModeloCarro = pedido.Automovel?.Modelo,
                PlacaCarro =  pedido.Automovel?.Placa,
                FotoCarro =   pedido.Automovel?.FotoUrl,

                Duracao = pedido.Duracao,
                TipoContrato = pedido.TipoContrato
            };

            return Ok(dto);
        }

       /// <summary>
        /// Aprova um pedido pendente.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <returns>Mensagem de sucesso</returns>
        [HttpPut("pedidos/{id}/aprovar")]
        public async Task<IActionResult> AprovarPedido(int id)
        {
            var agenteId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var pedido = await _context.Pedidos
                .Include(p => p.Automovel)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) return NotFound("Pedido não encontrado.");
            if (pedido.Status != "pendente")
                return BadRequest($"Não é possível aprovar um pedido com status {pedido.Status}.");

            if (pedido.AgenteDesignadoId != agenteId)
                return Forbid("Este pedido não pertence ao agente logado.");

            pedido.Status = "aprovado";

            // Cria o contrato
            var contrato = new Contrato
            {
                PedidoId = pedido.Id,
                ClienteId = pedido.ContratanteId,
                AgenteId = agenteId,
                TipoContrato = pedido.TipoContrato ?? "credito",
                DataInicio = DateTime.UtcNow,
                DataFim = DateTime.UtcNow.AddDays(pedido.Duracao)
            };

            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();

            // Envia email
            var cliente = await _context.Clientes.FindAsync(pedido.ContratanteId);
            if (cliente != null)
            {
                await EnviarEmail(cliente.Email, "Seu contrato foi gerado", $"Olá {cliente.Nome}, seu contrato foi gerado e é válido até {contrato.DataFim:dd/MM/yyyy}.");
            }

            return Ok($"Pedido {pedido.Id} aprovado e contrato gerado.");
        }

        /// <summary>
        /// Nega um pedido pendente.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <returns>Mensagem de sucesso</returns>
        [HttpPut("pedidos/{id}/negar")]
        public async Task<IActionResult> NegarPedido(int id)
        {
            var agenteId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null) return NotFound("Pedido não encontrado.");
            if (pedido.Status != "pendente")
                return BadRequest($"Não é possível negar um pedido com status {pedido.Status}.");

            if (pedido.AgenteDesignadoId != agenteId)
                return Forbid("Este pedido não pertence ao agente logado.");

            pedido.Status = "negado";
            await _context.SaveChangesAsync();
            return Ok($"Pedido {pedido.Id} negado com sucesso.");
        }
    }

    // DTOS

    public class PedidoPendenteDTO
    {
        public int PedidoId { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string CpfCliente { get; set; } = string.Empty;
        public string MarcaCarro { get; set; } = string.Empty;
        public string ModeloCarro { get; set; } = string.Empty;
        public string? FotoCarro { get; set; }
    }

    public class DetalhePedidoDTO
    {
        public int PedidoId { get; set; }
        public string Status { get; set; } = string.Empty;

        // Cliente
        public string? NomeCliente { get; set; }
        public string? CpfCliente { get; set; }
        public string? ProfissaoCliente { get; set; }
        public List<RendimentoDTO> Rendimentos { get; set; } = new();

        // Automovel
        public int? AnoCarro { get; set; }
        public string? MarcaCarro { get; set; }
        public string? ModeloCarro { get; set; }
        public string? PlacaCarro { get; set; }
        public string? FotoCarro { get; set; }

        public int Duracao { get; set; }
        public string? TipoContrato { get; set; }
    }

    public class RendimentoDTO
    {
        public decimal Valor { get; set; }
        public string? Fonte { get; set; }
    }
}
