using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface IDisciplinaService
    {
        void AdicionarDisciplina();
        void AtualizarDisciplina();
        void RemoverDisciplina();
        void AlocarProfessor();
        List<DisciplinaModel> ListarDisciplinas();
        // ...
    }
}
