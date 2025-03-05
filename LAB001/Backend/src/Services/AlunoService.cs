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

        public async Task<AlunoModel> EfetuarMatricula(
            EfetuarMatriculaRequest efetuarMatriculaRequest
        )
        {
            AlunoModel aluno = await _alunoHelper.FindAlunoByNumeroDePessoa(
                efetuarMatriculaRequest.NumeroDePessoa
            );

            if (
                efetuarMatriculaRequest.Disciplinas == null
                || efetuarMatriculaRequest.Disciplinas.Count == 0
            )
            {
                throw new InvalidOperationException("A lista de disciplinas não pode estar vazia");
            }

            aluno.Matricula.PlanoDeEnsino ??= [];
            List<DisciplinaModel> disciplinas = efetuarMatriculaRequest.Disciplinas;
            foreach (var disciplina in disciplinas)
            {
                if (aluno.Matricula.PlanoDeEnsino.Contains(disciplina))
                {
                    throw new InvalidOperationException(
                        $"Aluno já está matriculado na disciplina {disciplina.Nome}"
                    );
                }
                aluno.Matricula.PlanoDeEnsino.Add(disciplina);
            }
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();

            return aluno;
        }

        public async Task<List<AlunoModel>> ListarAlunos()
        {
            List<AlunoModel> alunos = await _context.Alunos.ToListAsync();
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
    }
}
