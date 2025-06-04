using AutoMapper;
using EcoScale.src.Auth;
using Microsoft.EntityFrameworkCore;
using sme.src.Auth;
using sme.src.Data;
using sme.src.Middlewares.Exceptions;
using sme.src.Models;
using sme.src.Models.Empresa;
using sme.src.Public.DTOs;

namespace sme.src.Services
{
    public class AlunoService(AppDbContext _context, IMapper _mapper)
    {
        private readonly Jwt _jwt = new(_context);
        public async Task<CreationResponse<Aluno>> CreateAsync(AlunoCreationRequest request)
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

            return new CreationResponse<Aluno>
            {
                Entity = aluno,
                Token = _jwt.GenerateToken(aluno.Email, Role.Aluno),
            };
        }

        public async Task<CreationResponse<Aluno>> UpdateAsync(AlunoUpdateRequest request, int id)
        {
            if (request == null) throw new CustomArgumentNullException(nameof(request), "Aluno cannot be null.");

            var aluno = await _context.Alunos.Include(a => a.Instituicao).Include(a => a.Curso).FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new NotFoundException("Aluno not found.");

            if (request.CursoId.HasValue)
            {
                var curso = await _context.Cursos.FindAsync(request.CursoId.Value)
                    ?? throw new NotFoundException("Curso not found.");
                aluno.Curso = curso;
            }

            if (request.InstituicaoId.HasValue)
            {
                var instituicao = await _context.Instituicoes.FirstOrDefaultAsync(i => i.Id == request.InstituicaoId)
                    ?? throw new NotFoundException("Instituição not found.");
                aluno.Instituicao = instituicao;
            }

            _mapper.Map(request, aluno);
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
            return new CreationResponse<Aluno>
            {
                Entity = aluno,
                Token = _jwt.GenerateToken(aluno.Email, Role.Aluno),
            };
        }

        public async Task<TransacaoResponse<EmpresaParceira>> ComprarProduto(TransacaoEmpresaRequest transacao)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.Id == transacao.AlunoId)
                ?? throw new NotFoundException("Aluno not found.");

            var empresa = await _context.EmpresasParceiras.FirstOrDefaultAsync(e => e.Id == transacao.EmpresaParceiraId)
                ?? throw new NotFoundException("Empresa Parceira not found.");

            ICollection<string> avisos = [];
            if (aluno.Moedas < transacao.Valor)
            {
                throw new BadRequestException("Saldo insuficiente para realizar a compra.");
            }

            var produtos = _context.Produtos
                .Where(p => transacao.Produtos.Any(tp => tp.ProdutoId == p.Id && p.Empresa.Id == empresa.Id))
                .ToList();
                
            if (produtos.Count == 0) {
                throw new NotFoundException("Nenhum produto encontrado para a transação.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var produto in produtos)
                {
                    if (produto.Quantidade <= 0)
                    {
                        avisos.Add($"Produto com id: {produto.Id} não está disponível no momento.");
                        continue;
                    }
                    if (produto.Quantidade < produto.Quantidade)
                    {
                        avisos.Add($"Quantidade solicitada para o produto  com id: {produto.Id} é maior do que a disponível.");
                        continue;
                    }
                    produto.Quantidade -= produto.Quantidade;
                    _context.Produtos.Update(produto);
                }

                var t = new TransacaoEmpresa
                {
                    EmpresaParceira = empresa,
                    Aluno = aluno,
                    Motivo = transacao.Motivo,
                    Valor = transacao.Valor,
                    DataTransacao = DateTime.Now,
                    Produtos = produtos,
                };

                _context.TransacoesAlunoEmpresa.Add(t);
                _context.Alunos.Update(aluno);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new TransacaoResponse<EmpresaParceira>
                {
                    Id = t.Id,
                    Aluno = aluno,
                    Motivo = t.Motivo,
                    Valor = t.Valor,
                    Ref = empresa,
                    Produtos = produtos,
                    DataTransacao = t.DataTransacao,
                    Avisos = avisos
                };
            } catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Erro ao realizar a transação: " + ex.Message);
            }
        }
    }
}