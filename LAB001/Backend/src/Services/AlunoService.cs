using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;
using Backend.src.services.Helpers;
using Microsoft.EntityFrameworkCore;
using InvalidOperationException = Backend.src.Middlewares.Exceptions.InvalidOperationException;

namespace Backend.src.services
{
    public class AlunoService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;
        private readonly AlunoHelper _alunoHelper = new(context);

        public async Task AdicionarAluno(AlunoModel aluno)
        {
            if (await _context.Alunos.AnyAsync(a => a.NumeroDePessoa == aluno.NumeroDePessoa))
            {
                throw new InvalidOperationException("Aluno com este número de pessoa já existe.");
            }

            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task<AlunoModel> CancelarMatricula(
            CancelarMatriculaRequest cancelarMatriculaRequest
        )
        {
            if (
                !cancelarMatriculaRequest.NumeroDeMatricula.HasValue
                && !cancelarMatriculaRequest.NumeroDePessoa.HasValue
            )
            {
                throw new BadRequestException(
                    "É obrigatorio o número de matricula ou o número de pessoa do aluno."
                );
            }

            AlunoModel aluno = cancelarMatriculaRequest.NumeroDePessoa.HasValue
                ? await _alunoHelper.FindAlunoByNumeroDePessoa(
                    cancelarMatriculaRequest.NumeroDePessoa.Value
                )
                : await _alunoHelper.FindAlunoByNumeroDeMatricula(
                    cancelarMatriculaRequest.NumeroDeMatricula.GetValueOrDefault()
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

        public async Task<ResponsePrecoSemestre> GetPrecoSemestre(GetPrecoSemestre getPrecoSemestre)
        {
            AlunoModel aluno = await _alunoHelper.FindAlunoByNumeroDePessoa(
                getPrecoSemestre.NumeroDePessoa
            );

            int preco = aluno.Matricula.PlanoDeEnsino?.Sum(disciplina => disciplina.Preco) ?? 0;

            return new ResponsePrecoSemestre { Preco = preco };
        }
    }
}
