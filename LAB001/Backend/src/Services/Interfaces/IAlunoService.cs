using Backend.src.DTOs;
using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface IAlunoService
    {
        Task AdicionarAluno(AlunoModel aluno);
        Task AtualizarAluno(string id);
        Task<AlunoModel> Login(LoginRequest loginRequest);
        Task RemoverAluno();
        Task EfetuarMatricula();
        Task CancelarMatricula();
        List<AlunoModel> ListarAlunos();
        // ...
    }
}