using Backend.src.DTOs;
using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface IAlunoService
    {
        Task AdicionarAluno(AlunoModel aluno);
        Task<AlunoModel> RemoverAluno(RemoverAlunoRequest removerAlunoRequest);
        Task<AlunoModel> EfetuarMatricula(AlunoModel aluno);
        Task<AlunoModel> CancelarMatricula(CancelarMatriculaRequest cancelarMatriculaRequest);
        Task<List<AlunoModel>> ListarAlunos();
        Task<ResponsePrecoSemestre> GetPrecoSemestre(string NumeroDePessoa);
        // ...
    }
}
