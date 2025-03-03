using Backend.src.Data;
using Backend.src.DTOs.ProfessorDTO;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;
using Backend.src.services.Helpers;
using Backend.src.services.interfaces;

namespace Backend.src.services
{
    public class ProfessorService(AppDbContext context) : IProfessorService
    {
        private readonly AppDbContext _context = context;
        private readonly ProfessorHelper _professorHelper = new(context);

        public void AdicionarProfessor()
        {
            throw new NotImplementedException();
        }

        public async Task<DisciplinaModel> AlocarDisciplina(
            AlocarDisciplinaRequest alocarDisciplinaRequest
        )
        {
            DisciplinaModel Disciplina = alocarDisciplinaRequest.Disciplina;
            int numeroDePessoa = alocarDisciplinaRequest.NumeroDePessoa;

            ProfessorModel professor = await _professorHelper.FindProfessorByNumeroDePessoa(
                numeroDePessoa
            );

            professor.Disciplinas.Add(Disciplina);
            Disciplina.Professor = professor;
            _context.Disciplinas.Update(Disciplina);
            _context.Professores.Update(professor);
            return Disciplina;
        }

        public void AtualizarProfessor()
        {
            throw new NotImplementedException();
        }

        public List<ProfessorModel> ListarProfessores()
        {
            throw new NotImplementedException();
        }

        public void RemoverProfessor()
        {
            throw new NotImplementedException();
        }
    }
}
