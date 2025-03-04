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

        public async Task<CurriculoModel> AdicionarDisciplina(
            AdicionarDisciplinaRequest adicionarDisciplinaRequest
        )
        {
            CurriculoModel curriculo = await _curriculoHelper.GetCurriculoById(
                adicionarDisciplinaRequest.CurriculoId
            );
            await _authService.FindAdminByNumero(adicionarDisciplinaRequest.NumeroDePessoa);
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
