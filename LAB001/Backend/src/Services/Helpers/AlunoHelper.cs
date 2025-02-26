using Backend.src.Data;
using Backend.src.Middleware.Exceptions;
using Backend.src.models;

namespace Backend.src.services.Helpers
{
    public class AlunoHelper(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<AlunoModel> FindAlunoByNumeroDePessoa(int numeroDePessoa)
        {
            return await _context.Alunos.FindAsync(numeroDePessoa) ?? throw new NotFoundException($"Aluno com o numero de pessoa {numeroDePessoa} não encontrado");
        }
    }
}