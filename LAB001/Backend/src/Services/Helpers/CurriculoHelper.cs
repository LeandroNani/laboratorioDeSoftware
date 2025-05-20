using Backend.src.Data;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.services.Helpers
{
    public class CurriculoHelper(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<CurriculoModel> GetCurriculoById(string CurriculoId)
        {
            return await _context
                    .Curriculos.Where(c => c.Id == CurriculoId)
                    .Include(c => c.Alunos)
                    .Include(c => c.Professores)
                    .Include(c => c.Disciplinas)
                    .Include(c => c.Cursos)
                    .FirstOrDefaultAsync()
                ?? throw new NotFoundException($"Curriculo com id {CurriculoId} não encontrado");
        }

        public async Task UpdateCurriculo(CurriculoModel curriculo)
        {
            var curriculoExistente =
                await _context
                    .Curriculos.Include(c => c.Cursos)
                    .Include(c => c.Alunos)
                    .Include(c => c.Professores)
                    .Include(c => c.Disciplinas)
                    .FirstOrDefaultAsync(c => c.Id == curriculo.Id)
                ?? throw new NotFoundException($"Curriculo com id {curriculo.Id} não encontrado");
            if (curriculo.Cursos != null)
            {
                foreach (var curso in curriculoExistente.Cursos)
                {
                    var cursoExistente = await _context.Cursos.FindAsync(curso.Id);
                    if (cursoExistente != null)
                    {
                        curriculoExistente.Cursos.Add(cursoExistente);
                    }
                }
            }
            if (curriculo.Professores != null)
            {
                foreach (var professor in curriculo.Professores)
                {
                    var professorExistente = await _context.Professores.FindAsync(
                        professor.NumeroDePessoa
                    );
                    if (professorExistente != null)
                    {
                        curriculoExistente.Professores.Add(professorExistente);
                    }
                }
            }
            if (curriculo.Alunos != null)
            {
                foreach (var aluno in curriculo.Alunos)
                {
                    var alunoExistente = await _context.Alunos.FindAsync(aluno.NumeroDePessoa);
                    if (alunoExistente != null)
                    {
                        curriculoExistente.Alunos.Add(alunoExistente);
                    }
                }
            }
            if (curriculo.Disciplinas != null)
            {
                foreach (var disciplina in curriculoExistente.Disciplinas)
                {
                    var disciplinaExistente = await _context.Disciplinas.FindAsync(disciplina.Id);
                    if (disciplinaExistente != null)
                    {
                        curriculoExistente.Disciplinas.Add(disciplinaExistente);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
