using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sme.src.Models;
using sme.src.Middlewares;
using sme.src.Data;
using sme.src.Services;
using sme.src.Public.DTOs;
using AutoMapper;

namespace sme.src.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar as operações relacionadas a Instituicão de ensino.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InstituicaoController(AppDbContext context, IMapper _mapper) : ControllerBase
    {
        private readonly Service<InstituicaoEnsino> _service = new (context);

        /// <summary>
        /// Obtém uma Instituicao pelo ID.
        /// </summary>
        /// <param name="id">ID do Instituicao.</param>
        /// <returns>O Instituicao correspondente ao ID fornecido.</returns>
        /// <response code="200">Retorna o Instituicao encontrado.</response>
        /// <response code="404">Se o Instituicao não for encontrado.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InstituicaoEnsino), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var _Instituicao = await _service.GetByIdAsync(id);
            return Ok(_Instituicao);
        }

        /// <summary>
        /// Obtém todas as Instituicoes.
        /// </summary>
        /// <returns>Uma lista de todos os Instituicoes.</returns>
        /// <response code="200">Retorna a lista de Instituicoes.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<InstituicaoEnsino>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var Instituicoes = await _service.GetAllAsync();
            return Ok(Instituicoes);
        }

        /// <summary>
        /// Cria uma nova Instituicao.
        /// </summary>
        /// <param name="request">A Instituicao a ser criado.</param>
        /// <returns>O Instituicao criado.</returns>
        /// <response code="201">Retorna a Instituicao criada</response>
        /// <response code="400">Se a Instituicao não for válida.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(InstituicaoEnsino), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] InstituicaoEnsinoCreationRequest request)
        {
            var instituicao = _mapper.Map<InstituicaoEnsino>(request);
            var response = await _service.AddAsync(instituicao);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(InstituicaoEnsino), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] InstituicaoEnsino request)
        {
            var entity = await _service.UpdateAsync(request, id);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
