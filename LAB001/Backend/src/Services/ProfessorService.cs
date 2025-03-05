using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.models;
using Backend.src.services.Helpers;
using Backend.src.services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.services
{
    public class ProfessorService(AppDbContext context) : IProfessorService
    {
        private readonly AppDbContext _context = context;
        private readonly ProfessorHelper _professorHelper = new(context);

        public async Task AdicionarProfessor(ProfessorModel professor)
        {
            ProfessorModel? existingProfessor = await _context.Professores.FindAsync(
                professor.NumeroDePessoa
            );
            if (existingProfessor != null)
                throw new Middlewares.Exceptions.InvalidOperationException(
                    $"Professor com o Numero de Pessoa {professor.NumeroDePessoa} já existe"
                );
            _context.Professores.Add(professor);
            await _context.SaveChangesAsync();
        }

        public async Task<DisciplinaModel> AlocarDisciplina(
            AlocarDisciplinaRequest alocarDisciplinaRequest
        )
        {
            DisciplinaModel Disciplina = alocarDisciplinaRequest.Disciplina;
            string numeroDePessoa = alocarDisciplinaRequest.NumeroDePessoa;

            ProfessorModel professor = await _professorHelper.FindProfessorByNumeroDePessoa(
                numeroDePessoa
            );

            professor.Disciplinas.Add(Disciplina);
            Disciplina.Professor = professor;
            _context.Disciplinas.Update(Disciplina);
            _context.Professores.Update(professor);
            await _context.SaveChangesAsync();
            return Disciplina;
        }

        public async Task AtualizarProfessor(ProfessorModel professorAtualizado)
        {
            ProfessorModel? professorExistente =
                await _context.Professores.FindAsync(professorAtualizado.NumeroDePessoa)
                ?? throw new Middlewares.Exceptions.NotFoundException(
                    $"Professor com NumeroDePessoa {professorAtualizado.NumeroDePessoa} não encontrado."
                );
            _context.Entry(professorExistente).CurrentValues.SetValues(professorAtualizado);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ProfessorModel>> ListarProfessores()
        {
            return await _context
                .Professores.Include(p => p.Disciplinas)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task RemoverProfessor(string numeroDePessoa)
        {
            ProfessorModel? professor =
                await _context.Professores.FindAsync(numeroDePessoa)
                ?? throw new Middlewares.Exceptions.NotFoundException(
                    $"Professor com NumeroDePessoa {numeroDePessoa} não encontrado."
                );
            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();
        }
    }
}
