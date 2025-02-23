using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    using System.Collections.Generic;

    [Table("curriculo")]
    public class CurriculoModel
    {
        public required List<AlunoModel> Alunos { get; set; }
        public required List<DisciplinaModel> Disciplinas { get; set; }
        public required List<ProfessorModel> Professores { get; set; }
        public required List<CursoModel> Curso { get; set; }
        public required string Semestre { get; set; }

        public CurriculoModel() { }
    }
}
