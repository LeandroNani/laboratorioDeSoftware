using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.models;
using Backend.src.services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.controllers
{
    [ApiController]
    [Route("[controller]")] // Define a rota como /professor
    public class ProfessorController(AppDbContext context) : ControllerBase
    {
        private readonly ProfessorService _professorService = new(context);

        [HttpPost("alocar-disciplina")]
        public async Task<IActionResult> AlocarProfessor(
            AlocarDisciplinaRequest alocarDisciplinaRequest
        )
        {
            await _professorService.AlocarDisciplina(alocarDisciplinaRequest);
            return Ok(StatusCode(200));
        }

        [HttpPost("novo-professor")]
        public async Task<IActionResult> NovoProfessor(ProfessorModel professor)
        {
            await _professorService.AdicionarProfessor(professor);
            return Ok(professor);
        }
    }
}
