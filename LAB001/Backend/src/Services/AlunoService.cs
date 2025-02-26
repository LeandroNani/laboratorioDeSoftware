using Backend.src.models;
using Backend.src.services.interfaces;
using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.Middleware.Exceptions;
using Backend.src.services.Helpers;
using Microsoft.EntityFrameworkCore;
using InvalidOperationException = Backend.src.Middleware.Exceptions.InvalidOperationException;

namespace Backend.src.services
{
    public class AlunoService(AppDbContext context) : IAlunoService
    {
        private readonly AppDbContext _context = context;
        private readonly AlunoHelper _alunoHelper = new (context);

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
            var aluno = await _context.Alunos.FindAsync(numeroDePessoa) ?? throw new NotFoundException($"Aluno com número de pessoa {numeroDePessoa} não encontrado");
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
        }

        public Task CancelarMatricula()
        {
            throw new NotImplementedException();
        }

        public async Task<AlunoModel> EfetuarMatricula(EfetuarMatriculaRequest efetuarMatriculaRequest)
        {
            var aluno = await _alunoHelper.FindAlunoByNumeroDePessoa(efetuarMatriculaRequest.NumeroDePessoa);

            if (efetuarMatriculaRequest.Disciplinas == null || efetuarMatriculaRequest.Disciplinas.Count == 0)
            {
                throw new InvalidOperationException("A lista de disciplinas não pode estar vazia");
            }

            aluno.PlanoDeEnsino ??= [];
            aluno.PlanoDeEnsino.AddRange(efetuarMatriculaRequest.Disciplinas);

            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();

            return aluno;
        }
        // TODO: listagem de alunos
        public List<AlunoModel> ListarAlunos()
        {
            throw new NotImplementedException();
        }

        public async Task<AlunoModel> Login(LoginRequest loginRequest)
        {
            var aluno = await _alunoHelper.FindAlunoByNumeroDePessoa(loginRequest.NumeroDePessoa);
            if (aluno == null || !loginRequest.Senha.Equals(aluno.Senha))
            {
                throw new InvalidPasswordException("Senha incorreta");
            }
            return aluno;
        }
        // TODO: remoção de um aluno
        public Task RemoverAluno()
        {
            throw new NotImplementedException();
        }
    }
}