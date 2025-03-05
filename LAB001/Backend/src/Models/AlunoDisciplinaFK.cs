using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.src.models;

namespace Backend.src.models
{
    [Table("aluno_disciplina")]
    public class AlunoDisciplina
    {
        [Key]
        public required string AlunoId { get; set; }

        [Required]
        [ForeignKey("aluno_id")]
        public required AlunoModel Aluno { get; set; }

        [Key]
        public required string DisciplinaId { get; set; }

        [Required]
        [ForeignKey("disciplina_id")]
        public required DisciplinaModel Disciplina { get; set; }
    }
}
