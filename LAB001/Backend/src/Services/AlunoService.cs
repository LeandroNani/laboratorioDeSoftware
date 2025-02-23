using Backend.src.models;
using Backend.src.services.interfaces;
using Backend.src.Data;
using System.Threading.Tasks;

namespace Backend.src.services
{
    public class AlunoService(AppDbContext context) : IAlunoService
    {
        private readonly AppDbContext _context = context;

        public async Task AdicionarAluno(AlunoModel aluno)
        {
            await _context.Alunos.AddAsync(aluno);
        }

        public async Task AtualizarAluno(string id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Update(aluno);
            }
            else
            {
                throw new KeyNotFoundException($"Aluno com id {id} não encontrado");
            }
        }

        public Task CancelarMatricula()
        {
            throw new NotImplementedException();
        }

        public Task EfetuarMatricula()
        {
            throw new NotImplementedException();
        }

        public List<AlunoModel> ListarAlunos()
        {
            throw new NotImplementedException();
        }

        public Task RemoverAluno()
        {
            throw new NotImplementedException();
        }
    }
}