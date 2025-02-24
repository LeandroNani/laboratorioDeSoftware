using Backend.src.models;
using Microsoft.AspNetCore.Mvc;
using Backend.src.services;
using System.Threading.Tasks;
using Backend.src.Data;
namespace Backend.src.controllers
{
    [ApiController]
    [Route("[controller]")] // Define a rota como /aluno
    public class AlunoController(AppDbContext context) : ControllerBase {
        private readonly AppDbContext _context = context;

        [HttpPost]
        public async Task<IActionResult> AdicionarAluno(AlunoModel aluno)
        {
            await new AlunoService(_context).AdicionarAluno(aluno);
            return Ok(aluno);
        }
    }
}
