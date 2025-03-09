using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.Mailer;
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
                await _context.Cursos.FirstOrDefaultAsync(c =>
                    c.Id == (aluno.Curso != null ? aluno.Curso.Id : "0")
                ) ?? throw new InvalidOperationException("Curso não encontrado.");
            aluno.Curso = existingCurso;
            aluno.Matricula ??= new MatriculaModel
            {
                NumeroDeMatricula = new Random().Next(100000, 999999).ToString(),
                Ativa = true,
                PlanoDeEnsino = aluno.Curso.Disciplinas?.ToList() ?? [],
                Mensalidade = aluno.Curso.Disciplinas?.Sum(d => d.Preco) ?? 0,
            };
            aluno.Matricula.PlanoDeEnsino = aluno.Curso.Disciplinas?.ToList() ?? [];
            aluno.Matricula.Mensalidade = aluno.Matricula.PlanoDeEnsino?.Sum(d => d.Preco) ?? 0;
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();

            MailerService mailer = new();
            mailer.SendEmail(aluno.NumeroDePessoa, aluno.Email);
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

            if (aluno.Matricula?.Ativa == null)
            {
                throw new InvalidOperationException("Aluno não possui matrícula ativa.");
            }
            aluno.Matricula.Ativa = false;
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();

            return aluno;
        }

        public async Task<AlunoModel> EfetuarMatricula(AlunoModel aluno)
        {
            if (aluno.Curso == null)
            {
                throw new InvalidOperationException(
                    "Aluno não possui um curso associado para efetuar a matrícula."
                );
            }

            // Load the curso including its disciplinas so that they are tracked
            var curso =
                await _context
                    .Cursos.Include(c => c.Disciplinas)
                    .FirstOrDefaultAsync(c => c.Id == aluno.Curso.Id)
                ?? throw new InvalidOperationException("Curso não encontrado.");

            aluno.Curso = curso;

            aluno.Matricula ??= new MatriculaModel
            {
                NumeroDeMatricula = new Random().Next(100000, 999999).ToString(),
                Ativa = false,
                PlanoDeEnsino = new List<DisciplinaModel>(),
                Mensalidade = 0,
            };

            // Reinicia o plano de ensino para evitar duplicações e tracking duplicado
            aluno.Matricula.PlanoDeEnsino = new List<DisciplinaModel>();

            // Use disciplinas únicas para evitar adicionar a mesma entidade duas vezes
            var disciplinasUnicas = (curso.Disciplinas ?? new List<DisciplinaModel>())
                .GroupBy(d => d.Id)
                .Select(g => g.First());

            foreach (var disciplina in disciplinasUnicas)
            {
                // Tenta obter a instância já rastreada
                var trackedDisciplina =
                    _context.Disciplinas.Local.FirstOrDefault(d => d.Id == disciplina.Id)
                    ?? await _context.Disciplinas.FindAsync(disciplina.Id);

                if (trackedDisciplina == null)
                {
                    throw new InvalidOperationException(
                        $"Disciplina com Id {disciplina.Id} não foi encontrada no banco de dados."
                    );
                }

                aluno.Matricula.PlanoDeEnsino.Add(trackedDisciplina);

                // Realiza o mesmo procedimento para o Professor, se necessário
                if (trackedDisciplina.Professor != null)
                {
                    var professorKey = trackedDisciplina.Professor.NumeroDePessoa;
                    var trackedProfessor =
                        _context
                            .ChangeTracker.Entries<ProfessorModel>()
                            .Select(e => e.Entity)
                            .FirstOrDefault(p => p.NumeroDePessoa == professorKey)
                        ?? _context.Professores.Local.FirstOrDefault(p =>
                            p.NumeroDePessoa == professorKey
                        )
                        ?? await _context.Professores.FindAsync(professorKey);

                    if (trackedProfessor == null)
                    {
                        throw new InvalidOperationException(
                            $"Professor com número de pessoa {professorKey} não foi encontrado."
                        );
                    }

                    trackedDisciplina.Professor = trackedProfessor;
                }
            }

            aluno.Matricula.Ativa = true;
            aluno.Matricula.NumeroDeMatricula = new Random().Next(100000, 999999).ToString();
            aluno.Matricula.Mensalidade = aluno.Matricula.PlanoDeEnsino.Sum(d => d.Preco);

            // Ensure the new Matricula record is tracked (and inserted) by the context
            if (_context.Entry(aluno.Matricula).State == EntityState.Detached)
            {
                _context.Matriculas.Add(aluno.Matricula);
            }

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

            int preco = 0;
            if (aluno.Matricula != null && aluno.Matricula.PlanoDeEnsino != null)
            {
                preco = aluno.Matricula.PlanoDeEnsino.Sum(disciplina => disciplina.Preco);
            }

            return new ResponsePrecoSemestre { Preco = preco };
        }

        public AlunoModel UpdateAluno(AlunoModel aluno)
        {
            if (aluno.Matricula != null)
            {
                int preco = aluno.Matricula.PlanoDeEnsino?.Sum(disciplina => disciplina.Preco) ?? 0;
                aluno.Matricula.Mensalidade = preco;
            }
            _context.Alunos.Update(aluno);
            return aluno;
        }
    }
}
