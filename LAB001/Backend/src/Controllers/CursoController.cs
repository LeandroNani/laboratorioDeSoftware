using Backend.src.Data;
using Backend.src.models;
using Backend.src.services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CursoController(AppDbContext context) : ControllerBase
    {
        private readonly CursoService _cursoService = new(context);

        /// <summary>
        /// Cria um novo curso
        /// </summary>
        /// <param name="curso">Dados do curso</param>
        /// <returns>O novo curso criado</returns>
        /// <response code="200">Curso criado com sucesso</response>
        /// <response code="500">Erro interno</response>
        [HttpPost("novo-curso")]
        [ProducesResponseType(typeof(CursoModel), 200)]
        public IActionResult CriarCurso(CursoModel curso)
        {
            _cursoService.CriarCurso(curso);
            return Ok(curso);
        }

        /// <summary>
        /// Busca todos os cursos
        /// </summary>
        /// <returns>Todos os cursos</returns>
        /// <response code="200">Cursos retornados com sucesso</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("listar")]
        [ProducesResponseType(typeof(List<CursoModel>), 200)]
        public async Task<IActionResult> GetCursos()
        {
            List<CursoModel> cursos = await _cursoService.GetCursos();
            return Ok(cursos);
        }

        /// <summary>
        /// Atualiza um curso
        /// </summary>
        /// <param name="curso">Dados do curso</param>
        /// <returns>O curso atualizado</returns>
        /// <response code="200">Curso atualizado com sucesso</response>
        /// <response code="500">Erro interno</response>
        [HttpPut("atualizar-curso")]
        [ProducesResponseType(typeof(CursoModel), 200)]
        public IActionResult UpdateCurso(CursoModel curso)
        {
            _cursoService.UpdateCurso(curso);
            return Ok(curso);
        }
    }
}
