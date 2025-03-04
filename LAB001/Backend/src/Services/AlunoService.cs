using System.Threading.Tasks;
using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.DTOs.AlunoDTO;
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

            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();
        }

        // TODO: Fazer a lógica correta para atualização genérica de um aluno
        public async Task AtualizarAluno(int numeroDePessoa)
        {
            AlunoModel aluno = await _alunoHelper.FindAlunoByNumeroDePessoa(numeroDePessoa);
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
        }

        public Task CancelarMatricula()
        {
            throw new NotImplementedException();
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

            aluno.PlanoDeEnsino ??= [];
            List<DisciplinaModel> disciplinas = efetuarMatriculaRequest.Disciplinas;
            foreach (var disciplina in disciplinas)
            {
                if (aluno.PlanoDeEnsino.Contains(disciplina))
                {
                    throw new InvalidOperationException(
                        $"Aluno já está matriculado na disciplina {disciplina.Nome}"
                    );
                }
                aluno.PlanoDeEnsino.Add(disciplina);
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

        // TODO: remoção de um aluno
        public async Task<AlunoModel> RemoverAluno(RemoverAlunoRequest removerAlunoRequest)
        {
            AlunoModel aluno = await _alunoHelper.FindAlunoByNumeroDePessoa(
                removerAlunoRequest.NumeroDePessoa
            );
            _context.Alunos.Remove(aluno);
            return aluno;
        }

        public async Task<ResponsePrecoSemestre> GetPrecoSemestre(GetPrecoSemestre getPrecoSemestre)
        {
            AlunoModel aluno = await _alunoHelper.FindAlunoByNumeroDePessoa(
                getPrecoSemestre.NumeroDePessoa
            );

            int preco = aluno.PlanoDeEnsino?.Sum(disciplina => disciplina.Preco) ?? 0;

            return new ResponsePrecoSemestre { Preco = preco };
        }
    }
}
