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
            var existingCurso =
                _context
                    .Cursos.Include(c => c.Disciplinas!)
                    .ThenInclude(d => d.Professor)
                    .FirstOrDefault(c => c.Id == curso.Id)
                ?? throw new InvalidOperationException("Curso not found.");

            _context.Entry(existingCurso).CurrentValues.SetValues(curso);

            var updatedDisciplineIds = new HashSet<string>(
                curso.Disciplinas?.Select(d => d.Id) ?? Enumerable.Empty<string>()
            );

            foreach (
                var disciplina in (
                    existingCurso.Disciplinas ?? Enumerable.Empty<DisciplinaModel>()
                ).ToList()
            )
            {
                if (!updatedDisciplineIds.Contains(disciplina.Id))
                {
                    existingCurso.Disciplinas?.Remove(disciplina);
                }
            }

            foreach (var disciplina in curso.Disciplinas ?? Enumerable.Empty<DisciplinaModel>())
            {
                var existingDisciplina = (
                    existingCurso.Disciplinas ?? Enumerable.Empty<DisciplinaModel>()
                ).FirstOrDefault(d => d.Id == disciplina.Id);

                if (existingDisciplina != null)
                {
                    _context.Entry(existingDisciplina).CurrentValues.SetValues(disciplina);
                    _context.Entry(existingDisciplina).State = EntityState.Modified;
                }
                else
                {
                    if (disciplina.Professor != null)
                    {
                        var existingProfessor = _context
                            .Pessoas.OfType<ProfessorModel>()
                            .FirstOrDefault(p =>
                                p.NumeroDePessoa == disciplina.Professor.NumeroDePessoa
                            );

                        if (existingProfessor != null)
                        {
                            disciplina.Professor = existingProfessor;
                        }
                        else
                        {
                            _context.Pessoas.Attach(disciplina.Professor);
                        }
                    }

                    var disciplineInDb = _context.Disciplinas.FirstOrDefault(d =>
                        d.Id == disciplina.Id
                    );
                    if (disciplineInDb != null)
                    {
                        _context.Entry(disciplineInDb).CurrentValues.SetValues(disciplina);
                        existingCurso.Disciplinas.Add(disciplineInDb);
                    }
                    else
                    {
                        _context.Disciplinas.Add(disciplina);
                        existingCurso.Disciplinas.Add(disciplina);
                    }
                }
            }

            _context.Entry(existingCurso).State = EntityState.Modified;
            _context.SaveChanges();
            return existingCurso;
        }
    }
}
