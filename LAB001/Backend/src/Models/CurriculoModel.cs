using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [Table("curriculo")]
    public class CurriculoModel
    {
        [Key]
        public required string Id { get; set; } = new Random().Next(100000, 999999).ToString();

        [ForeignKey("aluno_id")]
        public required List<AlunoModel> Alunos { get; set; } = [];

        [ForeignKey("disciplina_id")]
        public required List<DisciplinaModel> Disciplinas { get; set; } = [];
        public required List<ProfessorModel> Professores { get; set; } = [];
        public required List<CursoModel> Cursos { get; set; }
        public required string Semestre { get; set; }
    }
}
