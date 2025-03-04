using Backend.src.Data;
using Backend.src.DTOs.DisciplinaDTO;
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
    }
}
