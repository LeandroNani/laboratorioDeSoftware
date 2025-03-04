using Backend.src.Data;
using Backend.src.models;
using Backend.src.services.interfaces;

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
    }
}
