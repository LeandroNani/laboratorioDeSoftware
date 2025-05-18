namespace sme.src.Public.DTOs
{
    public class CursoCreationRequest
    {
        public required string Nome { get; set; }
    }

    public class CursoUpdateRequest
    {
        public string? Nome { get; set; }
    }
}