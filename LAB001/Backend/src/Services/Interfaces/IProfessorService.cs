using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface IProfessorService
    {
        void AdicionarProfessor();
        void AtualizarProfessor();
        void RemoverProfessor();
        Task<DisciplinaModel> AlocarDisciplina(int numeroDePessoa, DisciplinaModel disciplina);
        List<ProfessorModel> ListarProfessores();
        // ...
    }
}
