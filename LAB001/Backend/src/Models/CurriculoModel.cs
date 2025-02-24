using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [Table("curriculo")]
    public class CurriculoModel
    {
        [Key]
        public required string Id { get; set; }
        public required List<AlunoModel> Alunos { get; set; }
        public required List<DisciplinaModel> Disciplinas { get; set; }
        public required List<ProfessorModel> Professores { get; set; }
        public required List<CursoModel> Curso { get; set; }
        public required string Semestre { get; set; }

        public CurriculoModel() { }
    }
}
