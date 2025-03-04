using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.DTOs.AlunoDTO;
using Backend.src.models;
using Backend.src.services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.controllers
{
    [ApiController]
    [Route("[controller]")] // Define a rota como /aluno
    public class AlunoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AlunoService _alunoService;

        public AlunoController(AppDbContext context)
        {
            _context = context;
            _alunoService = new AlunoService(_context);
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> AdicionarAluno(AlunoModel aluno)
        {
            await _alunoService.AdicionarAluno(aluno);
            return Ok(aluno);
        }

        [HttpGet("preco-semestre")]
        public async Task<IActionResult> GetPrecoSemestre(GetPrecoSemestre getPrecoSemestre)
        {
            ResponsePrecoSemestre response = await _alunoService.GetPrecoSemestre(getPrecoSemestre);
            return Ok(response);
        }
    }
}
