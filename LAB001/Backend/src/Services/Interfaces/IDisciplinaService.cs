using Backend.src.DTOs;
using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface IDisciplinaService
    {
        Task<DisciplinaModel> AdicionarDisciplina(DisciplinaModel disciplina);
        Task AtualizarDisciplina(DisciplinaModel disciplinaAtualizada);
        Task RemoverDisciplina(string disciplinaId);
        Task AlocarProfessor(string disciplinaId, string numeroDePessoa);
        Task<List<DisciplinaModel>> ListarDisciplinas();
        // ...
    }
}
