using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    [Table("disciplina")]
    public class DisciplinaModel
    {
        public required string Id { get; set; }
        public required string Nome { get; set; }
        public required bool IsActive { get; set; }
        public required ProfessorModel Professor { get; set; }
        public required List<AlunoModel> Alunos { get; set; }
        public required int Precos { get; set; }
        public required string Periodo { get; set; }
        public required List<DisciplinaModel> DisciplinasNecessarias { get; set; }
        public required string Campus { get; set; }

        public DisciplinaModel() { }
    }
}
