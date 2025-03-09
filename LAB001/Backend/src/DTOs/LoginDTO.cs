using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Backend.src.models;
using Backend.src.services;

namespace Backend.src.DTOs
{
    public class LoginRequest
    {
        [Required]
        public required string NumeroDePessoa { get; set; }

        [Required]
        public required string Senha { get; set; }
    }

    [method: SetsRequiredMembers]
    public class ProfessorResponse(
        ProfessorModel professor,
        List<DisciplinaModel> disciplinas,
        List<AlunoModel> alunos
    )
    {
        public required ProfessorModel Professor { get; set; } = professor;
        public required List<DisciplinaModel> Disciplinas { get; set; } = disciplinas;
        public required List<AlunoModel> Alunos { get; set; } = alunos;
    }
}
