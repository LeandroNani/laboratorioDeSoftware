namespace sme.src.Public.DTOs
{
    public class EmpresaParceiraCreationRequest
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }

    public class EmpresaParceiraUpdateRequest
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
    }

    public class EmpresaParceiraResponse
    {
        public int EmpresaParceiraId { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}