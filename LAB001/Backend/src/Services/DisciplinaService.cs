using Backend.src.Data;
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
        private readonly DisciplinaHelper _disciplinaHelper = new(context);

        public async Task<DisciplinaModel> AdicionarDisciplina(DisciplinaModel disciplina)
        {
            var professor =
                await _context.Professores.FindAsync(disciplina.ProfessorId)
                ?? throw new Middlewares.Exceptions.NotFoundException(
                    $"Professor com ID {disciplina.ProfessorId} não encontrado."
                );

            disciplina.Professor = professor;

            _context.Disciplinas.Add(disciplina);
            await _context.SaveChangesAsync();
            return disciplina;
        }

        public async Task AlocarProfessor(string disciplinaId, string numeroDePessoa)
        {
            var disciplina =
                await _context.Disciplinas.FindAsync(disciplinaId)
                ?? throw new Middlewares.Exceptions.NotFoundException(
                    $"Disciplina com ID {disciplinaId} não encontrada."
                );
            var professor =
                await _professorHelper.FindProfessorByNumeroDePessoa(numeroDePessoa)
                ?? throw new Middlewares.Exceptions.NotFoundException(
                    $"Professor com NumeroDePessoa {numeroDePessoa} não encontrado."
                );
            disciplina.Professor = professor;
            _context.Disciplinas.Update(disciplina);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarDisciplina(DisciplinaModel disciplinaAtualizada)
        {
            var disciplinaExistente =
                await _context.Disciplinas.FindAsync(disciplinaAtualizada.Id)
                ?? throw new Middlewares.Exceptions.NotFoundException(
                    $"Disciplina com ID {disciplinaAtualizada.Id} não encontrada."
                );
            _context.Entry(disciplinaExistente).CurrentValues.SetValues(disciplinaAtualizada);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DisciplinaModel>> ListarDisciplinas()
        {
            return await _context
                .Disciplinas.Include(d => d.Professor)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task RemoverDisciplina(string disciplinaId)
        {
            var disciplina =
                await _context.Disciplinas.FindAsync(disciplinaId)
                ?? throw new Middlewares.Exceptions.NotFoundException(
                    $"Disciplina com ID {disciplinaId} não encontrada."
                );
            _context.Disciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();
        }

        public async Task<DisciplinaModel> GetDisciplinaById(string id)
        {
            DisciplinaModel disciplina = await _disciplinaHelper.FindDisciplinaByid(id);
            return disciplina;
        }
    }
}
