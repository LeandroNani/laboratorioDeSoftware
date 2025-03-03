using Backend.src.Data;
using Backend.src.DTOs.ProfessorDTO;
using Backend.src.services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.controllers
{
    [ApiController]
    [Route("[controller]")] // Define a rota como /professor
    public class ProfessorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ProfessorService _professorService;

        public ProfessorController(AppDbContext context)
        {
            _context = context;
            _professorService = new ProfessorService(_context);
        }

        [HttpPost("alocar-disciplina")]
        public async Task<IActionResult> AlocarProfessor(
            AlocarDisciplinaRequest alocarDisciplinaRequest
        )
        {
            await _professorService.AlocarDisciplina(alocarDisciplinaRequest);
            return Ok(StatusCode(200));
        }
    }
}
