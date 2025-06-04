using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sme.src.Models;
using sme.src.Middlewares;
using sme.src.Data;
using sme.src.Services;
using sme.src.Public.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace sme.src.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar as operações relacionadas aos alunos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController(AppDbContext context, IMapper _mapper) : ControllerBase
    {
        private readonly Service<Aluno> _service = new(context);
        private readonly AlunoService _alunoService = new(context, _mapper);
        /// <summary>
        /// Obtém um aluno pelo ID.
        /// </summary>
        /// <param name="id">ID do aluno.</param>
        /// <returns>O aluno correspondente ao ID fornecido.</returns>
        /// <response code="200">Retorna o aluno encontrado.</response>
        /// <response code="404">Se o aluno não for encontrado.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AlunoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var aluno = await _service.GetByIdAsync(id);
            AlunoResponse _aluno = _mapper.Map<AlunoResponse>(aluno);
            return Ok(_aluno);
        }

        /// <summary>
        /// Obtém todos os alunos.
        /// </summary>
        /// <returns>Uma lista de todos os alunos.</returns>
        /// <response code="200">Retorna a lista de alunos.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<AlunoResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var alunos = await _service.GetAllAsync();
            var alunosResponse = _mapper.Map<IEnumerable<AlunoResponse>>(alunos);
            return Ok(alunosResponse);
        }

        /// <summary>
        /// Cria um novo aluno.
        /// </summary>
        /// <param name="request">O aluno a ser criado.</param>
        /// <returns>O aluno criado.</returns>
        /// <response code="201">Retorna o aluno criado.</response>
        /// <response code="400">Se o aluno não for válido.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(Aluno), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] AlunoCreationRequest request)
        {
            var aluno = await _alunoService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), new { aluno });
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(Aluno), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] AlunoUpdateRequest request)
        {
            var aluno = await _alunoService.UpdateAsync(request, id);
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Efetua a compra de um produto por um aluno.
        /// </summary>
        /// <param name="transacao">O aluno a ser criado.</param>
        /// <returns>O aluno criado.</returns>
        /// <response code="201">Retorna o aluno criado.</response>
        /// <response code="400">Se o aluno não for válido.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpPost("comprar-produto")]
        [Authorize(Policy = "AlunoPolicy")]
        [ProducesResponseType(typeof(TransacaoResponse<Transacao>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ComprarProduto([FromBody] TransacaoEmpresaRequest transacao)
        {
            var response = await _alunoService.ComprarProduto(transacao);
            return Ok(response);
        }

    }
}
