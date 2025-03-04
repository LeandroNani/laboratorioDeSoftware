using Backend.src.DTOs.DisciplinaDTO;
using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface IDisciplinaService
    {
        Task<CurriculoModel> AdicionarDisciplina(AdicionarDisciplinaRequest disciplina);
        void AtualizarDisciplina();
        void RemoverDisciplina();
        void AlocarProfessor();
        List<DisciplinaModel> ListarDisciplinas();
        // ...
    }
}
