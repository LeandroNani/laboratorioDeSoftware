using Backend.src.DTOs.ProfessorDTO;
using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface IProfessorService
    {
        Task AdicionarProfessor(ProfessorModel professor);
        void AtualizarProfessor();
        void RemoverProfessor();
        Task<DisciplinaModel> AlocarDisciplina(AlocarDisciplinaRequest alocarDisciplinaRequest);
        List<ProfessorModel> ListarProfessores();
        // ...
    }
}
