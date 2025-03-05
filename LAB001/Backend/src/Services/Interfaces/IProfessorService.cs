using Backend.src.DTOs;
using Backend.src.models;

namespace Backend.src.services.interfaces
{
public interface IProfessorService
{
    Task AdicionarProfessor(ProfessorModel professor);
    Task AtualizarProfessor(ProfessorModel professorAtualizado);
    Task RemoverProfessor(int numeroDePessoa);
    Task<DisciplinaModel> AlocarDisciplina(AlocarDisciplinaRequest alocarDisciplinaRequest);
    Task<List<ProfessorModel>> ListarProfessores();
}
}