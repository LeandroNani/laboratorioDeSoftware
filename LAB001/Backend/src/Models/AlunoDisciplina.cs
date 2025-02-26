using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.src.models;

namespace Backend.src.Models
{
    [Table("aluno_disciplina")]
    public class AlunoDisciplina
    {
        [Key]
        public int AlunoId { get; set; }

        [Required]
        public required AlunoModel Aluno { get; set; }

        [Key]
        public int DisciplinaId { get; set; }

        [Required]
        public required DisciplinaModel Disciplina { get; set; }
    }
}