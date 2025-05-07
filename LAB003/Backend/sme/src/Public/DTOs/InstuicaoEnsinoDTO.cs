namespace sme.src.Public.DTOs
{
    public class InstituicaoEnsinoCreationRequest
    {
        public required string Nome { get; set; }
    }

    public class InstituicaoEnsinoUpdateRequest
    {
        public string? Nome { get; set; }
    }
}