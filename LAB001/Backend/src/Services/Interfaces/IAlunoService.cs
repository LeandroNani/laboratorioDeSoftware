using Backend.src.DTOs;
using Backend.src.DTOs.AlunoDTO;
using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface IAlunoService
    {
        Task AdicionarAluno(AlunoModel aluno);
        Task AtualizarAluno(int id);
        Task RemoverAluno();
        Task<AlunoModel> EfetuarMatricula(EfetuarMatriculaRequest efetuarMatriculaRequest);
        Task CancelarMatricula();
        List<AlunoModel> ListarAlunos();
        Task<ResponsePrecoSemestre> GetPrecoSemestre(GetPrecoSemestre getPrecoSemestre);
        // ...
    }
}
