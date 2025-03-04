using Backend.src.Data;
using Backend.src.models;

namespace Backend.src.services
{
    public class CursoService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public CursoModel CriarCurso(CursoModel curso)
        {
            _context.Cursos.Add(curso);
            return curso;
        }
    }
}
