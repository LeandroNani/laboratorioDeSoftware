using Backend.src.Data;
using Backend.src.DTOs.DisciplinaDTO;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;
using Backend.src.services.Auth;
using Backend.src.services.Helpers;
using Backend.src.services.interfaces;

namespace Backend.src.services
{
    public class DisciplinaService(AppDbContext context) : IDisciplinaService
    {
        private readonly AppDbContext _context = context;
        private readonly AuthService _authService = new(context);
        private readonly CurriculoHelper _curriculoHelper = new(context);
        private readonly ProfessorHelper _professorHelper = new(context);

        public async Task<CurriculoModel> AdicionarDisciplina(
            AdicionarDisciplinaRequest adicionarDisciplinaRequest
        )
        {
            var curriculoTask = _curriculoHelper.GetCurriculoById(
                adicionarDisciplinaRequest.CurriculoId
            );
            var professorTask = _professorHelper.FindProfessorByNumeroDePessoa(
                adicionarDisciplinaRequest.Disciplina.Professor.NumeroDePessoa
            );
            var adminTask = _authService.FindAdminByNumero(
                adicionarDisciplinaRequest.NumeroDePessoa
            );

            await Task.WhenAll(curriculoTask, professorTask, adminTask);

            var curriculo = await curriculoTask;
            var professor = await professorTask;

            adicionarDisciplinaRequest.Disciplina.Professor = professor;
            curriculo.Disciplinas.Add(adicionarDisciplinaRequest.Disciplina);

            _curriculoHelper.UpdateCurriculo(curriculo);

            return curriculo;
        }

        public void AlocarProfessor()
        {
            throw new NotImplementedException();
        }

        public void AtualizarDisciplina()
        {
            throw new NotImplementedException();
        }

        public List<DisciplinaModel> ListarDisciplinas()
        {
            throw new NotImplementedException();
        }

        public void RemoverDisciplina()
        {
            throw new NotImplementedException();
        }
    }
}
