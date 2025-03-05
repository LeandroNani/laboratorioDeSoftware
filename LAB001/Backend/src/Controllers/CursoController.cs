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

        [HttpPost("novo-curso")]
        public IActionResult CriarCurso(CursoModel curso)
        {
            _cursoService.CriarCurso(curso);
            return Ok(curso);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> GetCursos()
        {
            List<CursoModel> cursos = await _cursoService.GetCursos();
            return Ok(cursos);
        }

        [HttpPut("atualizar-curso")]
        public IActionResult UpdateCurso(CursoModel curso)
        {
            _cursoService.UpdateCurso(curso);
            return Ok(curso);
        }
    }
}
