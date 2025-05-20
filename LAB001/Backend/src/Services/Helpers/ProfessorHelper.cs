using Backend.src.Data;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;

namespace Backend.src.services.Helpers
{
    public class ProfessorHelper(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<ProfessorModel> FindProfessorByNumeroDePessoa(string numeroDePessoa)
        {
            ProfessorModel professor =
                await _context.Professores.FindAsync(numeroDePessoa)
                ?? throw new NotFoundException(
                    $"Professor com o numero de pessoa {numeroDePessoa} não encontrado"
                );
            return professor;
        }
    }
}
