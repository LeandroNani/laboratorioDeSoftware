using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [Table("curriculo")]
    public class CurriculoModel
    {
        [Key]
        public required string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("aluno_id")]
        public required List<AlunoModel> Alunos { get; set; } = [];

        [ForeignKey("disciplina_id")]
        public required List<DisciplinaModel> Disciplinas { get; set; } = [];

        [ForeignKey("professor_id")]
        public required List<ProfessorModel> Professores { get; set; } = [];

        [ForeignKey("curso_id")]
        public required CursoModel Curso { get; set; }
        public required string Semestre { get; set; }
    }
}
