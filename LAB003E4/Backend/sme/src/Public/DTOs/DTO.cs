namespace sme.src.Public.DTOs
{
    public class CreationResponse<T> where T : class
    {
        public required T Entity { get; set; }
        public string? Token { get; set; }
    }
}