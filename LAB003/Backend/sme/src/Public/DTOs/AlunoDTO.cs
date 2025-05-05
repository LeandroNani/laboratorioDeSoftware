using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sme.src.Models;

namespace sme.src.Public.DTOs
{
    public class AlunoCreationRequest
    {
        public required string Rg { get; set; }
        public required string Cpf { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required InstituicaoEnsino Instituicao { get; set; }
        public required Curso Curso { get; set; }
        public int? Moedas { get; set; }
    }

    public class AlunoUpdateRequest
    {
        public string? Rg { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public InstituicaoEnsino? Instituicao { get; set; }
        public Curso? Curso { get; set; }
        public int? Moedas { get; set; }
    }

    public class AlunoResponse
    {
        public required int AlunoId { get; set; }
        public required string Rg { get; set; }
        public required string Cpf { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required InstituicaoEnsino Instituicao { get; set; }
        public required Curso Curso { get; set; }
        public required int Moedas { get; set; }
    }
}