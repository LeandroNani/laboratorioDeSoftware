using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface IProfessorService
    {
        void AdicionarProfessor();
        void AtualizarProfessor();
        void RemoverProfessor();
        void AlocarDisciplina();
        List<ProfessorModel> ListarProfessores();
        // ...
    }
}