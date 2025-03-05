using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.models;
using Backend.src.services.Auth;
using Backend.src.services.Helpers;
using Backend.src.services.interfaces;
using Microsoft.EntityFrameworkCore;

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
            await _context.SaveChangesAsync();
            return curriculo;
        }

        public async Task AlocarProfessor(int disciplinaId, int numeroDePessoa)
        {
            var disciplina = await _context.Disciplinas.FindAsync(disciplinaId);
            if (disciplina == null)
            {
                throw new Middlewares.Exceptions.NotFoundException($"Disciplina com ID {disciplinaId} não encontrada.");
            }

            var professor = await _professorHelper.FindProfessorByNumeroDePessoa(numeroDePessoa);
            if (professor == null)
            {
                throw new Middlewares.Exceptions.NotFoundException($"Professor com NumeroDePessoa {numeroDePessoa} não encontrado.");
            }

            disciplina.Professor = professor;
            _context.Disciplinas.Update(disciplina);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarDisciplina(DisciplinaModel disciplinaAtualizada)
        {
            var disciplinaExistente = await _context.Disciplinas.FindAsync(disciplinaAtualizada.Id);

            if (disciplinaExistente == null)
            {
                throw new Middlewares.Exceptions.NotFoundException($"Disciplina com ID {disciplinaAtualizada.Id} não encontrada.");
            }

            _context.Entry(disciplinaExistente).CurrentValues.SetValues(disciplinaAtualizada);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DisciplinaModel>> ListarDisciplinas()
        {
            return await _context.Disciplinas
                .Include(d => d.Professor)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task RemoverDisciplina(int disciplinaId)
        {
            var disciplina = await _context.Disciplinas.FindAsync(disciplinaId);

            if (disciplina == null)
            {
                throw new Middlewares.Exceptions.NotFoundException($"Disciplina com ID {disciplinaId} não encontrada.");
            }

            _context.Disciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();
        }
    }
}