using sme.src.Models;
using sme.src.Models.Empresa;

namespace sme.src.Public.DTOs
{
    public class TransacaoCreationRequest<T>
    {
        public required decimal Valor { get; set; }
        public required string Motivo { get; set; }
        public required T Ref { get; set; }
        public required Aluno Aluno { get; set; }
        public Produto? Produto { get; set; }
    }
    public class TransacaoResponse<T>
    {
        public int Id { get; set; }
        public required decimal Valor { get; set; }
        public required string Motivo { get; set; }
        public required Aluno Aluno { get; set; }
        public required T Ref { get; set; }
        public required ICollection<Produto> Produtos { get; set; }
        public required DateTime DataTransacao { get; set; }
        public ICollection<string>? Avisos { get; set; } = [];
    }

    public class TransacaoEmpresaRequest
    {
        public required decimal Valor { get; set; }
        public required string Motivo { get; set; }
        public required int EmpresaParceiraId { get; set; }
        public required int AlunoId { get; set; }
        public required ICollection<ComprarProdutoRequest> Produtos { get; set; } = [];
    }
}