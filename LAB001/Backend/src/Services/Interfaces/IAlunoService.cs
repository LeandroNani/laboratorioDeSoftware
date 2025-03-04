using Backend.src.DTOs;
using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface IAlunoService
    {
        Task AdicionarAluno(AlunoModel aluno);
        Task<AlunoModel> RemoverAluno(RemoverAlunoRequest removerAlunoRequest);
        Task<AlunoModel> EfetuarMatricula(EfetuarMatriculaRequest efetuarMatriculaRequest);
        Task CancelarMatricula();
        Task<List<AlunoModel>> ListarAlunos();
        Task<ResponsePrecoSemestre> GetPrecoSemestre(GetPrecoSemestre getPrecoSemestre);
        // ...
    }
}
