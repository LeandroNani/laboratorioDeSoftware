using Backend.src.Data;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.services.Helpers
{
    public class AlunoHelper(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<AlunoModel> FindAlunoByNumeroDePessoa(int numeroDePessoa)
        {
            return await _context.Alunos.FindAsync(numeroDePessoa)
                ?? throw new NotFoundException(
                    $"Aluno com o numero de pessoa {numeroDePessoa} não encontrado"
                );
        }

        public async Task<AlunoModel> FindAlunoByNumeroDeMatricula(int numeroDeMatricula)
        {
            var aluno = await _context
                .Alunos.Include(a => a.Matricula)
                .FirstOrDefaultAsync(a => a.Matricula.NumeroDeMatricula == numeroDeMatricula);

            return aluno
                ?? throw new NotFoundException(
                    $"Aluno com o numero de matricula {numeroDeMatricula} não encontrado"
                );
        }
    }
}
