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
            curso.Disciplinas = _context.Disciplinas.ToList();
            _context.Cursos.Add(curso);
            _context.SaveChanges();
            return curso;
        }

        public async Task<List<CursoModel>> GetCursos()
        {
            List<CursoModel> cursos = await _context.Cursos.ToListAsync();
            return cursos;
        }

        public CursoModel UpdateCurso(CursoModel curso)
        {
            curso.Disciplinas = _context.Disciplinas.ToList();
            _context.Cursos.Update(curso);
            _context.SaveChanges();
            return curso;
        }
    }
}
