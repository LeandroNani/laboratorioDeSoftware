using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;
using Backend.src.services.Helpers;
using Backend.src.services.interfaces;
using Microsoft.EntityFrameworkCore;
using InvalidOperationException = Backend.src.Middlewares.Exceptions.InvalidOperationException;

namespace Backend.src.services
{
    public class AlunoService(AppDbContext context) : IAlunoService
    {
        private readonly AppDbContext _context = context;
        private readonly AlunoHelper _alunoHelper = new(context);

        public async Task AdicionarAluno(AlunoModel aluno)
        {
            if (await _context.Alunos.AnyAsync(a => a.NumeroDePessoa == aluno.NumeroDePessoa))
            {
                throw new InvalidOperationException("Aluno com este número de pessoa já existe.");
            }

            var existingCurso =
                await _context.Cursos.FirstOrDefaultAsync(c => c.Id == aluno.Curso.Id)
                ?? throw new InvalidOperationException("Curso não encontrado.");
            aluno.Curso = existingCurso;

            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task<AlunoModel> GetAlunoByNumeroDePessoa(string numeroDePessoa)
        {
            AlunoModel aluno = await _alunoHelper.FindAlunoByNumeroDePessoa(numeroDePessoa);
            return aluno;
        }

        public async Task<AlunoModel> CancelarMatricula(
            CancelarMatriculaRequest cancelarMatriculaRequest
        )
        {
            if (
                string.IsNullOrEmpty(cancelarMatriculaRequest.NumeroDeMatricula)
                && string.IsNullOrEmpty(cancelarMatriculaRequest.NumeroDePessoa)
            )
            {
                throw new BadRequestException(
                    "É obrigatorio o número de matricula ou o número de pessoa do aluno."
                );
            }

            AlunoModel aluno = !string.IsNullOrEmpty(cancelarMatriculaRequest.NumeroDePessoa)
                ? await _alunoHelper.FindAlunoByNumeroDePessoa(
                    cancelarMatriculaRequest.NumeroDePessoa
                )
                : await _alunoHelper.FindAlunoByNumeroDeMatricula(
                    cancelarMatriculaRequest.NumeroDeMatricula
                        ?? throw new BadRequestException(
                            nameof(cancelarMatriculaRequest.NumeroDeMatricula)
                        )
                );

            aluno.Matricula.Ativa = false;
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();

            return aluno;
        }

        public async Task<AlunoModel> EfetuarMatricula(AlunoModel aluno)
        {
            var disciplinasNovas = aluno.Matricula.PlanoDeEnsino ??= new List<DisciplinaModel>();
            aluno.Matricula.PlanoDeEnsino?.Clear();

            foreach (var disciplina in disciplinasNovas)
            {
                var localDisciplina = _context.Disciplinas.Local.FirstOrDefault(d =>
                    d.Id == disciplina.Id
                );
                var disciplinaToAdd =
                    localDisciplina ?? await _context.Disciplinas.FindAsync(disciplina.Id);

                if (disciplinaToAdd == null)
                {
                    disciplinaToAdd = disciplina;
                }
                else if (_context.Entry(disciplinaToAdd).State == EntityState.Detached)
                {
                    _context.Disciplinas.Attach(disciplinaToAdd);
                }

                if (
                    aluno.Matricula.PlanoDeEnsino != null
                    && aluno.Matricula.PlanoDeEnsino.Any(d => d.Id == disciplinaToAdd.Id)
                )
                {
                    throw new InvalidOperationException(
                        $"Aluno já está matriculado na disciplina {disciplinaToAdd.Nome}"
                    );
                }
                aluno.Matricula.PlanoDeEnsino?.Add(disciplinaToAdd);
                if (disciplinaToAdd.Professor != null)
                {
                    var professorKey = disciplinaToAdd.Professor.NumeroDePessoa;

                    var trackedProfessor = _context
                        .ChangeTracker.Entries<ProfessorModel>()
                        .Select(e => e.Entity)
                        .FirstOrDefault(p => p.NumeroDePessoa == professorKey);

                    if (trackedProfessor != null)
                    {
                        disciplinaToAdd.Professor = trackedProfessor;
                    }
                    else
                    {
                        trackedProfessor =
                            _context.Professores.Local.FirstOrDefault(p =>
                                p.NumeroDePessoa == professorKey
                            ) ?? await _context.Professores.FindAsync(professorKey);

                        if (trackedProfessor != null)
                        {
                            disciplinaToAdd.Professor = trackedProfessor;
                        }
                        else
                        {
                            _context.Professores.Attach(disciplinaToAdd.Professor);
                        }
                    }
                }
            }

            aluno.Matricula.Mensalidade = aluno.Matricula.PlanoDeEnsino?.Sum(d => d.Preco) ?? 0;

            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();

            return aluno;
        }

        public async Task<List<AlunoModel>> ListarAlunos()
        {
            List<AlunoModel> alunos = await _context
                .Alunos.Include(m => m.Matricula)
                .Include(a => a.Curso)
                .ToListAsync();
            return alunos;
        }

        public async Task<AlunoModel> RemoverAluno(RemoverAlunoRequest removerAlunoRequest)
        {
            AlunoModel aluno = await _alunoHelper.FindAlunoByNumeroDePessoa(
                removerAlunoRequest.NumeroDePessoa
            );
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return aluno;
        }

        public async Task<ResponsePrecoSemestre> GetPrecoSemestre(string NumeroDePessoa)
        {
            AlunoModel aluno = await _alunoHelper.FindAlunoByNumeroDePessoa(NumeroDePessoa);

            int preco = aluno.Matricula.PlanoDeEnsino?.Sum(disciplina => disciplina.Preco) ?? 0;

            return new ResponsePrecoSemestre { Preco = preco };
        }

        public AlunoModel UpdateAluno(AlunoModel aluno)
        {
            int preco = aluno.Matricula.PlanoDeEnsino?.Sum(disciplina => disciplina.Preco) ?? 0;
            aluno.Matricula.Mensalidade = preco;
            _context.Alunos.Update(aluno);
            return aluno;
        }
    }
}
