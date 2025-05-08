using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sme.src.Data;
using sme.src.Middlewares.Exceptions;
using sme.src.Models;
using sme.src.Public.DTOs;

namespace sme.src.Services
{
    public class AlunoService(AppDbContext _context, IMapper _mapper)
    {
        public async Task<Aluno> CreateAsync(AlunoCreationRequest request)
        {
            if (request == null) throw new CustomArgumentNullException(nameof(request), "Aluno cannot be null.");
            var existingAluno = await _context.Alunos.FirstOrDefaultAsync(a => a.Cpf == request.Cpf);

            if (existingAluno != null) throw new ConflictException("Aluno already exists with the same data.");

            var instituicao = await _context.Instituicoes.FirstOrDefaultAsync(i => i.Id == request.InstituicaoId)
                ?? throw new NotFoundException("Instituição not found.");
            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == request.CursoId)
                ?? throw new NotFoundException("Curso not found.");

            var aluno = _mapper.Map<Aluno>(request);
            aluno.Instituicao = instituicao;
            aluno.Curso = curso;
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }


        public async Task<Aluno> UpdateAsync(AlunoUpdateRequest request, int id)
        {
            if (request == null) throw new CustomArgumentNullException(nameof(request), "Aluno cannot be null.");

            var aluno = await _context.Alunos.Include(a => a.Instituicao).Include(a => a.Curso).FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new NotFoundException("Aluno not found.");

            if (request.CursoId.HasValue) {
                var curso = await _context.Cursos.FindAsync(request.CursoId.Value)
                    ?? throw new NotFoundException("Curso not found.");
                aluno.Curso = curso;
            }

            if(request.InstituicaoId.HasValue) {
                var instituicao = await _context.Instituicoes.FirstOrDefaultAsync(i => i.Id == request.InstituicaoId)
                    ?? throw new NotFoundException("Instituição not found.");
                aluno.Instituicao = instituicao;
            }

            _mapper.Map(request, aluno);
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }
    }
}