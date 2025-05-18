using sme.src.Models;

namespace sme.src.Public.DTOs
{
    public class ProfessorCreationRequest
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string Cpf { get; set; }
        public required int DepartamentoId { get; set; }
    }

    public class ProfessorUpdateRequest
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public int? DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }
        public int? Moedas { get; set; }
    }

    public class ProfessorResponse
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Cpf { get; set; }
        
        // mapeados via .ForMember no Profile
        public required string Departamento { get; set; }
        
        public int Moedas { get; set; }
        public DateTime LastAllocationDate { get; set; }
    }

    public class CoinTransferRequest
    {
        public required int AlunoId { get; set; }
        public required int Amount { get; set; }
        public required string Message { get; set; }
    }
}