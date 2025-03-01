using Backend.src.Data;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;
using Backend.src.services.interfaces;

namespace Backend.src.services
{
    public class ProfessorService(AppDbContext context) : IProfessorService
    {
        private readonly AppDbContext _context = context;

        public void AdicionarProfessor()
        {
            throw new NotImplementedException();
        }

        public async Task<DisciplinaModel> AlocarDisciplina(
            int numeroDePessoa,
            DisciplinaModel disciplina
        )
        {
            ProfessorModel professor =
                await _context.Professores.FindAsync(numeroDePessoa)
                ?? throw new NotFoundException(
                    $"Professor com o número de pessoa {numeroDePessoa} não encontrado"
                );
            professor.Disciplinas.Add(disciplina);
            disciplina.Professor = professor;
            _context.Disciplinas.Update(disciplina);
            _context.Professores.Update(professor);
            return disciplina;
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
