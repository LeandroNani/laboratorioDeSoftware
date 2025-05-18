using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sme.src.Data;
using sme.src.Middlewares.Exceptions;
using sme.src.Models;
using sme.src.Public.DTOs;

namespace sme.src.Services
{
    public class ProfessorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public ProfessorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Professor> CreateAsync(ProfessorCreationRequest request)
        {
            if (request == null) throw new CustomArgumentNullException(nameof(request), "O parâmetro de requisição não pode ser nulo.");
            var exists = await _context.Professores.FirstOrDefaultAsync(p => p.Cpf == request.Cpf);
            if (exists != null) throw new ConflictException("Professor já existe com este CPF.");

            var dept = await _context.Departamentos.FindAsync(request.DepartamentoId)
                       ?? throw new NotFoundException("Departamento não encontrado.");

            var professor = _mapper.Map<Professor>(request);
            professor.Departamento = dept;
            professor.Moedas = 0;
            professor.LastAllocationDate = DateTime.UtcNow;

            await _context.Professores.AddAsync(professor);
            await _context.SaveChangesAsync();
            return professor;
        }

        public async Task<Professor> UpdateAsync(int id, ProfessorUpdateRequest request)
        {
            if (request == null) throw new CustomArgumentNullException(nameof(request), "O parâmetro de requisição não pode ser nulo.");
            var professor = await _context.Professores.Include(p => p.Departamento)
                             .FirstOrDefaultAsync(p => p.Id == id)
                             ?? throw new NotFoundException("Professor não encontrado.");

            if (request.DepartamentoId.HasValue)
            {
                var dept = await _context.Departamentos.FindAsync(request.DepartamentoId.Value)
                           ?? throw new NotFoundException("Departamento não encontrado.");
                professor.Departamento = dept;
            }

            _mapper.Map(request, professor);
            _context.Professores.Update(professor);
            await _context.SaveChangesAsync();
            return professor;
        }

        public async Task SendCoinsAsync(int professorId, CoinTransferRequest request)
        {
            var prof = await _context.Professores.FindAsync(professorId)
                       ?? throw new NotFoundException("Professor não encontrado.");

            await AllocatePendingSemesterCoins(prof);

            if (prof.Moedas < request.Amount)
                throw new BadRequestException("Saldo insuficiente para enviar moedas.");

            var aluno = await _context.Alunos.FindAsync(request.AlunoId)
                        ?? throw new NotFoundException("Aluno não encontrado.");

            prof.Moedas -= request.Amount;
            aluno.Moedas += request.Amount;
            await _context.SaveChangesAsync();
        }

        public async Task AllocateSemesterCoinsAsync()
        {
            var now = DateTime.UtcNow;
            var all = await _context.Professores.ToListAsync();
            foreach (var prof in all)
                await AllocatePendingSemesterCoins(prof);
        }

        private async Task AllocatePendingSemesterCoins(Professor prof)
        {
            var now = DateTime.UtcNow;
            int sems = CalculateSemestersBetween(prof.LastAllocationDate, now);
            if (sems > 0)
            {
                prof.Moedas += sems * 1000;
                prof.LastAllocationDate = StartOfCurrentSemester(now);
                _context.Professores.Update(prof);
                await _context.SaveChangesAsync();
            }
        }

        private int CalculateSemestersBetween(DateTime last, DateTime now)
        {
            int lastSem = (last.Year * 2) + (last.Month <= 6 ? 1 : 2);
            int nowSem = (now.Year * 2) + (now.Month <= 6 ? 1 : 2);
            return nowSem - lastSem;
        }

        private DateTime StartOfCurrentSemester(DateTime now)
            => now.Month <= 6 ? new DateTime(now.Year, 1, 1) : new DateTime(now.Year, 7, 1);
    }
}