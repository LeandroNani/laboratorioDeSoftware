using Backend.src.Data;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;

namespace Backend.src.services.Helpers
{
    public class CurriculoHelper(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<CurriculoModel> GetCurriculoById(string CurriculoId)
        {
            return await _context.Curriculos.FindAsync(CurriculoId)
                ?? throw new NotFoundException($"Curriculo com id {CurriculoId} não encontrado");
        }

        public void UpdateCurriculo(CurriculoModel curriculo)
        {
            _context.Curriculos.Update(curriculo);
        }
    }
}
