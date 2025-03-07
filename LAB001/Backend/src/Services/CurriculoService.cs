using Backend.src.Data;
using Backend.src.models;
using Backend.src.services.Helpers;
using Backend.src.services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.services
{
    public class CurriculoService(AppDbContext context) : ICurriculoService
    {
        private readonly AppDbContext _context = context;
        private readonly CurriculoHelper _curriculoHelper = new(context);

        public async Task<CurriculoModel> CriarCurriculo(CurriculoModel curriculo)
        {
            _context.Add(curriculo);
            await _context.SaveChangesAsync();
            return curriculo;
        }

        public async Task<List<CurriculoModel>> ListarCurriculos()
        {
            return await _context.Curriculos.ToListAsync();
        }

        public async Task<CurriculoModel> GetCurriculo()
        {
            CurriculoModel curriculo = await _curriculoHelper.GetCurriculoById("2025.1");
            return curriculo;
        }

        public async Task UpdateCurriculo(CurriculoModel curriculo)
        {
            await _curriculoHelper.UpdateCurriculo(curriculo);
        }
    }
}
