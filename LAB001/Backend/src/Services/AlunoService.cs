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
                Paga = false,
            };
            aluno.Matricula.PlanoDeEnsino = aluno.Curso.Disciplinas?.ToList() ?? [];
            aluno.Matricula.Mensalidade = aluno.Matricula.PlanoDeEnsino?.Sum(d => d.Preco) ?? 0;
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();

            MailerService mailer = new();
            mailer.SendEmail(aluno.NumeroDePessoa, aluno.Email, aluno.Nome, aluno.Type);
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
            var existingAluno =
                await _context.Alunos.FindAsync(aluno.NumeroDePessoa)
                ?? throw new InvalidOperationException("Aluno não encontrado.");

            existingAluno.DisciplinasCursadas = [];

            if (aluno.Matricula?.PlanoDeEnsino != null)
            {
                if (existingAluno.Matricula == null)
                {
                    existingAluno.Matricula = new MatriculaModel
                    {
                        NumeroDeMatricula = new Random().Next(100000, 999999).ToString(),
                        Ativa = true,
                        Mensalidade = 0,
                        PlanoDeEnsino = [],
                        Paga = false,
                    };
                }
                else if (existingAluno.Matricula.PlanoDeEnsino == null)
                {
                    existingAluno.Matricula.PlanoDeEnsino = new List<DisciplinaModel>();
                }
                foreach (var disciplina in aluno.Matricula.PlanoDeEnsino)
                {
                    var disciplinaExistente = await _context.Disciplinas.FindAsync(disciplina.Id);
                    if (disciplinaExistente != null)
                    {
                        existingAluno.Matricula.PlanoDeEnsino.Add(disciplinaExistente);
                    }
                }
            }
            existingAluno.Matricula.Mensalidade =
                existingAluno.Matricula?.PlanoDeEnsino?.Sum(d => d.Preco) ?? 0;
            await _context.SaveChangesAsync();
            return existingAluno;
        }

        public async Task<List<AlunoModel>> ListarAlunos()
        {
            List<AlunoModel> alunos = await _context
                .Alunos.Include(m => m.Matricula)
                .ThenInclude(p => p.PlanoDeEnsino)
                .ThenInclude(p => p.Professor)
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

        public async Task<AlunoModel> UpdateAluno(AlunoModel aluno)
        {
            var existingAluno =
                await _context
                    .Alunos.Include(a => a.Matricula)
                    .ThenInclude(m => m.PlanoDeEnsino)
                    .FirstOrDefaultAsync(a => a.NumeroDePessoa == aluno.NumeroDePessoa)
                ?? throw new InvalidOperationException("Aluno não encontrado.");

            // Limpa a coleção de disciplinas cursadas (se for uma coleção gerenciada, use Clear())
            existingAluno.DisciplinasCursadas.Clear();

            // Lista para acumular as disciplinas a remover do PlanoDeEnsino
            var disciplinasParaRemover = new List<DisciplinaModel>();

            if (aluno.DisciplinasCursadas != null)
            {
                // Itera sobre uma cópia da lista para evitar conflitos
                foreach (var disciplina in aluno.DisciplinasCursadas.ToList())
                {
                    var disciplinaExistente = await _context.Disciplinas.FindAsync(disciplina.Id);
                    if (disciplinaExistente != null)
                    {
                        // Adiciona a disciplina à coleção do aluno
                        existingAluno.DisciplinasCursadas.Add(disciplinaExistente);

                        // Cria uma cópia do PlanoDeEnsino para evitar modificação durante enumeração
                        var planoDeEnsinoSnapshot = existingAluno.Matricula?.PlanoDeEnsino.ToList();
                        var disciplinaToRemove = planoDeEnsinoSnapshot?.FirstOrDefault(d =>
                            d.Id == disciplinaExistente.Id
                        );
                        if (disciplinaToRemove != null)
                        {
                            // Acumula para remoção posterior
                            disciplinasParaRemover.Add(disciplinaToRemove);
                        }
                    }
                }
            }

            // Após a iteração, remove todas as disciplinas acumuladas do PlanoDeEnsino
            foreach (var item in disciplinasParaRemover)
            {
                existingAluno.Matricula?.PlanoDeEnsino.Remove(item);
            }

            existingAluno.Matricula.Mensalidade =
                existingAluno.Matricula?.PlanoDeEnsino?.Sum(d => d.Preco) ?? 0;
            existingAluno.Matricula.Paga = aluno.Matricula.Paga;
            existingAluno.Email = aluno.Email;
            existingAluno.Nome = aluno.Nome;
            await _context.SaveChangesAsync();
            return existingAluno;
        }
    }
}
