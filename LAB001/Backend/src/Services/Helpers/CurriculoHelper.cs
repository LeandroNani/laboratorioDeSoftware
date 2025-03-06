using Backend.src.Data;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.services.Helpers
{
    public class CurriculoHelper(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<CurriculoModel> GetCurriculoById(string CurriculoId)
        {
            return await _context
                    .Curriculos.Where(c => c.Id == CurriculoId)
                    .Include(c => c.Alunos)
                    .Include(c => c.Professores)
                    .Include(c => c.Disciplinas)
                    .Include(c => c.Cursos)
                    .FirstOrDefaultAsync()
                ?? throw new NotFoundException($"Curriculo com id {CurriculoId} não encontrado");
        }

        public async Task UpdateCurriculo(CurriculoModel curriculo)
        {
            var curriculoExistente =
                await _context
                    .Curriculos.Include(c => c.Cursos)
                    .Include(c => c.Alunos)
                    .Include(c => c.Professores)
                    .Include(c => c.Disciplinas)
                    .FirstOrDefaultAsync(c => c.Id == curriculo.Id)
                ?? throw new NotFoundException($"Curriculo com id {curriculo.Id} não encontrado");

            // Atualiza as propriedades do objeto principal
            _context.Entry(curriculoExistente).CurrentValues.SetValues(curriculo);

            // --- Atualização de Professores ---
            var professoresUnicos = curriculo
                .Professores.GroupBy(p => p.NumeroDePessoa)
                .Select(g => g.First())
                .ToList();

            foreach (var professor in professoresUnicos)
            {
                var existingProfessor = curriculoExistente.Professores.FirstOrDefault(p =>
                    p.NumeroDePessoa == professor.NumeroDePessoa
                );

                if (existingProfessor != null)
                {
                    _context.Entry(existingProfessor).CurrentValues.SetValues(professor);
                }
                else
                {
                    var trackedProfessor = _context.Professores.Local.FirstOrDefault(p =>
                        p.NumeroDePessoa == professor.NumeroDePessoa
                    );
                    if (trackedProfessor != null)
                    {
                        _context.Entry(trackedProfessor).CurrentValues.SetValues(professor);
                        if (
                            !curriculoExistente.Professores.Any(p =>
                                p.NumeroDePessoa == professor.NumeroDePessoa
                            )
                        )
                        {
                            curriculoExistente.Professores.Add(trackedProfessor);
                        }
                    }
                    else
                    {
                        var dbProfessor = await _context.Professores.FindAsync(
                            professor.NumeroDePessoa
                        );
                        if (dbProfessor != null)
                        {
                            _context.Entry(dbProfessor).CurrentValues.SetValues(professor);
                            curriculoExistente.Professores.Add(dbProfessor);
                        }
                        else
                        {
                            curriculoExistente.Professores.Add(professor);
                        }
                    }
                }
            }
            // Remove professores que não estão na lista de entrada
            foreach (var professorExistente in curriculoExistente.Professores.ToList())
            {
                if (
                    !professoresUnicos.Any(p =>
                        p.NumeroDePessoa == professorExistente.NumeroDePessoa
                    )
                )
                {
                    _context.Professores.Remove(professorExistente);
                }
            }

            // Atualização e adição de cursos
            foreach (var curso in curriculo.Cursos)
            {
                var cursoExistenteNoContexto =
                    _context.Cursos.Local.FirstOrDefault(c => c.Id == curso.Id)
                    ?? await _context.Cursos.FindAsync(curso.Id);
                if (cursoExistenteNoContexto != null)
                {
                    _context.Entry(cursoExistenteNoContexto).CurrentValues.SetValues(curso);
                    // Se necessário, adicione ao currículo se ainda não estiver presente
                    if (!curriculoExistente.Cursos.Any(c => c.Id == curso.Id))
                    {
                        curriculoExistente.Cursos.Add(cursoExistenteNoContexto);
                    }
                }
                else
                {
                    curriculoExistente.Cursos.Add(curso);
                }
            }

            // Remoção de cursos que não estão na lista de entrada
            foreach (var cursoExistente in curriculoExistente.Cursos.ToList())
            {
                if (!curriculo.Cursos.Any(c => c.Id == cursoExistente.Id))
                {
                    _context.Cursos.Remove(cursoExistente);
                }
            }

            // --- Atualização de Alunos ---
            var alunosUnicos = curriculo
                .Alunos.GroupBy(a => a.NumeroDePessoa)
                .Select(g => g.First())
                .ToList();

            foreach (var aluno in alunosUnicos)
            {
                var existingAluno = curriculoExistente.Alunos.FirstOrDefault(a =>
                    a.NumeroDePessoa == aluno.NumeroDePessoa
                );
                if (existingAluno != null)
                {
                    _context.Entry(existingAluno).CurrentValues.SetValues(aluno);
                }
                else
                {
                    var trackedAluno = _context.Alunos.Local.FirstOrDefault(a =>
                        a.NumeroDePessoa == aluno.NumeroDePessoa
                    );
                    if (trackedAluno != null)
                    {
                        _context.Entry(trackedAluno).CurrentValues.SetValues(aluno);
                        if (
                            !curriculoExistente.Alunos.Any(a =>
                                a.NumeroDePessoa == aluno.NumeroDePessoa
                            )
                        )
                        {
                            curriculoExistente.Alunos.Add(trackedAluno);
                        }
                    }
                    else
                    {
                        var dbAluno = await _context.Alunos.FindAsync(aluno.NumeroDePessoa);
                        if (dbAluno != null)
                        {
                            _context.Entry(dbAluno).CurrentValues.SetValues(aluno);
                            curriculoExistente.Alunos.Add(dbAluno);
                        }
                        else
                        {
                            curriculoExistente.Alunos.Add(aluno);
                        }
                    }
                }
            }
            // Remove alunos que não estão na lista de entrada
            foreach (var alunoExistente in curriculoExistente.Alunos.ToList())
            {
                if (!alunosUnicos.Any(a => a.NumeroDePessoa == alunoExistente.NumeroDePessoa))
                {
                    _context.Alunos.Remove(alunoExistente);
                }
            }

            // foreach (var disciplina in curriculo.Disciplinas)
            // {
            //     var disciplinaExistente = curriculoExistente.Disciplinas.FirstOrDefault(d =>
            //         d.Id == disciplina.Id
            //     );
            //     if (disciplinaExistente != null)
            //     {
            //         _context.Entry(disciplinaExistente).CurrentValues.SetValues(disciplina);
            //         // Certifique-se de atualizar a associação com o curso, se necessário:
            //         if (disciplina.Curso != null)
            //         {
            //             disciplinaExistente.Curso = curriculoExistente.Cursos.FirstOrDefault(c =>
            //                 c.Id == disciplina.Curso.Id
            //             );
            //         }
            //     }
            //     else
            //     {
            //         var disciplinaLocal =
            //             _context.Disciplinas.Local.FirstOrDefault(d => d.Id == disciplina.Id)
            //             ?? await _context.Disciplinas.FindAsync(disciplina.Id);
            //         if (disciplinaLocal != null)
            //         {
            //             _context.Entry(disciplinaLocal).CurrentValues.SetValues(disciplina);
            //             disciplinaLocal.Curso = curriculoExistente.Cursos.FirstOrDefault(c =>
            //                 c.Id == disciplina.Curso.Id
            //             );
            //             if (!curriculoExistente.Disciplinas.Any(d => d.Id == disciplina.Id))
            //             {
            //                 curriculoExistente.Disciplinas.Add(disciplinaLocal);
            //             }
            //         }
            //         else
            //         {
            //             // Antes de adicionar, certifique-se que disciplina.Curso está corretamente associado
            //             disciplina.Curso ??= curriculoExistente.Cursos.FirstOrDefault(c =>
            //                 c.Id == disciplina.Curso.Id
            //             );
            //             curriculoExistente.Disciplinas.Add(disciplina);
            //         }
            //     }
            // }
            await _context.SaveChangesAsync();
        }
    }
}
