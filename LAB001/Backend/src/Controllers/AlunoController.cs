using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.models;
using Backend.src.services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.controllers
{
    [ApiController]
    [Route("[controller]")] // Define a rota como /aluno
    public class AlunoController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly AlunoService _alunoService = new(context);

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

        [HttpPatch("cancelar-matricula")]
        public async Task<IActionResult> CancelarMatricula(
            CancelarMatriculaRequest cancelarMatriculaRequest
        )
        {
            AlunoModel aluno = await _alunoService.CancelarMatricula(cancelarMatriculaRequest);
            return Ok(aluno);
        }

        [HttpPatch("listar-alunos")]
        public async Task<IActionResult> ListarAlunos()
        {
            List<AlunoModel> alunos = await _alunoService.ListarAlunos();
            return Ok(alunos);
        }
    }
}
