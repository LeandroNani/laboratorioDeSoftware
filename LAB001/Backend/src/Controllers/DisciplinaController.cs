using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.models;
using Backend.src.services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.controllers
{
    [ApiController]
    [Route("[controller]")] // Define a rota como /disciplina
    public class DisciplinaController(AppDbContext context) : ControllerBase
    {
        private readonly DisciplinaService _disciplinaService = new(context);

        [HttpPost("nova-disciplina")]
        public async Task<IActionResult> NovaDisciplina(
            AdicionarDisciplinaRequest adicionarDisciplinaRequest
        )
        {
            CurriculoModel curriculo = await _disciplinaService.AdicionarDisciplina(
                adicionarDisciplinaRequest
            );
            return Ok(curriculo);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarDisciplinas()
        {
            List<DisciplinaModel> disciplinas = await _disciplinaService.ListarDisciplinas();
            return Ok(disciplinas);
        }

        [HttpGet("get-disciplina/{id}")]
        public async Task<IActionResult> GetDisciplina(string id)
        {
            DisciplinaModel disciplina = await _disciplinaService.GetDisciplinaById(id);
            return Ok(disciplina);
        }
    }
}
