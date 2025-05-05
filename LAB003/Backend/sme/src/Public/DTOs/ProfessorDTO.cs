using sme.src.Models;

namespace sme.src.Public.DTOs
{
    public class ProfessorUpdateRequest
    {
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public Departamento? Departamento { get; set; }
        public int? Moedas { get; set; }
    }

    public class ProfessorResponse
    {
        public required int ProfessorId { get; set; }
        public required string Nome { get; set; }
        public required string Senha { get; set; }
        public required string Cpf { get; set; }
        public required Departamento Departamento { get; set; }
        public required int Moedas { get; set; }
    }
}