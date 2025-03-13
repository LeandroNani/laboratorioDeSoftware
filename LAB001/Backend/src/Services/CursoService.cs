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
            var existingCurso = _context
                .Cursos.Include(c => c.Disciplinas!)
                .ThenInclude(d => d.Professor)
                .FirstOrDefault(c => c.Id == curso.Id);

            if (existingCurso != null)
            {
                throw new InvalidOperationException("Curso with the same ID already exists.");
            }

            foreach (var disciplina in curso.Disciplinas ?? Enumerable.Empty<DisciplinaModel>())
            {
                var existingDisciplina = _context
                    .Disciplinas.Include(d => d.Professor)
                    .FirstOrDefault(d => d.Id == disciplina.Id);
                if (existingDisciplina != null)
                {
                    curso.Disciplinas =
                    [
                        .. (curso.Disciplinas ?? Enumerable.Empty<DisciplinaModel>()).Select(d =>
                            d == disciplina ? existingDisciplina : d
                        ),
                    ];
                }
                else
                {
                    _context.Disciplinas.Attach(disciplina);
                }

                if (disciplina.Professor != null)
                {
                    var existingProfessor = _context.Pessoas.FirstOrDefault(p =>
                        p.NumeroDePessoa == disciplina.Professor.NumeroDePessoa
                    );
                    if (existingProfessor != null)
                    {
                        disciplina.Professor = (ProfessorModel)existingProfessor;
                    }
                    else
                    {
                        _context.Pessoas.Attach(disciplina.Professor);
                    }
                }
            }

            _context.Cursos.Add(curso);
            _context.SaveChanges();
            return curso;
        }

        public async Task<List<CursoModel>> GetCursos()
        {
            List<CursoModel> cursos = await _context
                .Cursos.Include(c => c.Disciplinas!)
                .ThenInclude(p => p.Professor)
                .ToListAsync();
            return cursos;
        }

        public CursoModel UpdateCurso(CursoModel curso)
        {
            // Retrieve the existing course along with its disciplines from the database.
            var existingCurso = _context
                .Cursos.Include(c => c.Disciplinas)
                .ThenInclude(d => d.Professor)
                .FirstOrDefault(c => c.Id == curso.Id);

            if (existingCurso == null)
                throw new InvalidOperationException("Curso not found.");

            // Update the scalar properties of the course.
            _context.Entry(existingCurso).CurrentValues.SetValues(curso);

            // Determine which discipline IDs are sent in the update.
            var updatedDisciplineIds = curso.Disciplinas?.Select(d => d.Id).ToHashSet() ?? [];

            // Remove disciplines from the current course that are not in the update.
            foreach (var disciplina in existingCurso.Disciplinas.ToList())
            {
                if (!updatedDisciplineIds.Contains(disciplina.Id))
                {
                    existingCurso.Disciplinas.Remove(disciplina);
                }
            }

            // Add new disciplines or update existing ones.
            foreach (var disciplina in curso.Disciplinas ?? Enumerable.Empty<DisciplinaModel>())
            {
                var existingDisciplina = existingCurso.Disciplinas.FirstOrDefault(d =>
                    d.Id == disciplina.Id
                );
                if (existingDisciplina != null)
                {
                    // Update the discipline properties.
                    _context.Entry(existingDisciplina).CurrentValues.SetValues(disciplina);
                }
                else
                {
                    // If the discipline is new, handle the related professor.
                    if (disciplina.Professor != null)
                    {
                        var existingProfessor = _context.Pessoas.FirstOrDefault(p =>
                            p.NumeroDePessoa == disciplina.Professor.NumeroDePessoa
                        );
                        if (existingProfessor != null)
                        {
                            disciplina.Professor = (ProfessorModel)existingProfessor;
                        }
                        else
                        {
                            _context.Pessoas.Attach(disciplina.Professor);
                        }
                    }
                    existingCurso.Disciplinas.Add(disciplina);
                }
            }

            _context.SaveChanges();
            return existingCurso;
        }
    }
}
