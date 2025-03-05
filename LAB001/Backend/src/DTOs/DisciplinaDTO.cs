using System.ComponentModel.DataAnnotations;
using Backend.src.models;

namespace Backend.src.DTOs
{
    public class AdicionarDisciplinaRequest
    {
        [Required]
        public required string NumeroDePessoa;

        [Required]
        public required DisciplinaModel Disciplina;

        [Required]
        public required string CurriculoId;
    }
}
