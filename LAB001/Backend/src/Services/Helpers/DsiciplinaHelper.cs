using Backend.src.Data;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.services.Helpers
{
    public class DisciplinaHelper(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<DisciplinaModel> FindDisciplinaByid(string id)
        {
            DisciplinaModel disciplina =
                await _context
                    .Disciplinas.Include(d => d.Professor)
                    .FirstOrDefaultAsync(d => d.Id == id)
                ?? throw new NotFoundException($"Disciplina com id {id} não encontrado");

            return disciplina;
        }
    }
}
