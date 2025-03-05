using Backend.src.Data;
using Backend.src.models;
using Backend.src.services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.services
{
    public class CursoService(AppDbContext context) : ICursoService
    {
        private readonly AppDbContext _context = context;

        public CursoModel CriarCurso(CursoModel curso)
        {
            _context.Cursos.Add(curso);
            _context.SaveChanges();
            return curso;
        }

        public async Task<List<CursoModel>> GetCursos()
        {
            List<CursoModel> cursos = await _context
                .Cursos.Include(d => d.Disciplinas)
                .ToListAsync();
            return cursos;
        }
    }
}
