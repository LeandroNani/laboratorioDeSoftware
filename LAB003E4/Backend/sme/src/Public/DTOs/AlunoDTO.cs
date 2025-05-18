using sme.src.Models;
// Add the correct namespace for CursoResponse and InstituicaoResponse if they exist
using sme.src.Public.DTOs; // Change this to the actual namespace where CursoResponse is defined
// Add the correct using for CursoResponse and InstituicaoResponse if they are in another namespace
// Example: using sme.src.OtherNamespace;
// Add the correct using for Moeda if it exists elsewhere
// Example: using sme.src.Models;

// If Moeda is not defined elsewhere, define it below:

namespace sme.src.Public.DTOs
{
    public class AlunoCreationRequest
    {
        public required string Rg { get; set; }
        public required string Cpf { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required int InstituicaoId { get; set; }
        public required int CursoId { get; set; }
        public int? Moedas { get; set; }
    }

    public class AlunoUpdateRequest
    {
        public string? Rg { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public int? InstituicaoId { get; set; }
        public int? CursoId { get; set; }
        public int? Moedas { get; set; }
    }

    public class AlunoResponse
    {
        public int Id { get; set; }
        public required string Rg { get; set; }
        public required string Email { get; set; }
        public int Moedas { get; set; }

        public required string Curso { get; set; }
        public required string Instituicao { get; set; }
    }
}


