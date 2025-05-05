using sme.src.Models.Empresa;

namespace sme.src.Public.DTOs
{
    public class ProdutoCreationRequest
    {
        public required byte[] Foto { get; set; }
        public required string Descricao { get; set; }
        public required decimal Preco { get; set; }
        public required EmpresaParceira Empresa { get; set; }
    }

    public class ProdutoUpdateRequest
    {
        public byte[]? Foto { get; set; }
        public string? Descricao { get; set; }
        public decimal? Preco { get; set; }
    }

    public class ProdutoResponse
    {
        public int ProdutoId { get; set; }
        public required byte[] Foto { get; set; }
        public required string Descricao { get; set; }
        public required decimal Preco { get; set; }
        public required EmpresaParceira Empresa { get; set; }
    }
}