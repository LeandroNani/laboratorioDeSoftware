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
    /// Controlador responsável por gerenciar as operações relacionadas aos Cursos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController(AppDbContext context, IMapper _mapper) : ControllerBase
    {
        private readonly Service<Curso> _service = new (context);

        /// <summary>
        /// Obtém um Curso pelo ID.
        /// </summary>
        /// <param name="id">ID do Curso.</param>
        /// <returns>O Curso correspondente ao ID fornecido.</returns>
        /// <response code="200">Retorna o Curso encontrado.</response>
        /// <response code="404">Se o Curso não for encontrado.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Curso), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var Curso = await _service.GetByIdAsync(id);
            return Ok(Curso);
        }

        /// <summary>
        /// Obtém todos os Cursos.
        /// </summary>
        /// <returns>Uma lista de todos os Cursos.</returns>
        /// <response code="200">Retorna a lista de Cursos.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<Curso>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var Cursos = await _service.GetAllAsync();
            var CursosResponse = _mapper.Map<IEnumerable<Curso>>(Cursos);
            return Ok(CursosResponse);
        }

        /// <summary>
        /// Cria um novo Curso.
        /// </summary>
        /// <param name="request">O Curso a ser criado.</param>
        /// <returns>O Curso criado.</returns>
        /// <response code="201">Retorna o Curso criado.</response>
        /// <response code="400">Se o Curso não for válido.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(Curso), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CursoCreationRequest request)
        {
            var Curso = _mapper.Map<Curso>(request);
            await _service.AddAsync(Curso);
            return CreatedAtAction(nameof(GetById), new { id = Curso.Id }, Curso);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(Curso), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] CursoUpdateRequest request)
        {
            var Curso = _mapper.Map<Curso>(request);
            await _service.UpdateAsync(Curso, id);
            return Ok(Curso);
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
