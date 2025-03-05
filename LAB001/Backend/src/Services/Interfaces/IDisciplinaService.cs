using Backend.src.DTOs;
using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface IDisciplinaService
    {
        Task<CurriculoModel> AdicionarDisciplina(AdicionarDisciplinaRequest disciplina);
        Task AtualizarDisciplina(DisciplinaModel disciplinaAtualizada);
        Task RemoverDisciplina(int disciplinaId);
        Task AlocarProfessor(int disciplinaId, int numeroDePessoa);
        Task<List<DisciplinaModel>> ListarDisciplinas();
        // ...
    }
}
