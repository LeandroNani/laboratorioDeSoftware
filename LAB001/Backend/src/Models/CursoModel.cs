using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    [Table("curso")]
    public class CursoModel
    {
        public required string Id { get; set; }
        public required string Nome { get; set; }
        public required List<DisciplinaModel> Disciplinas { get; set; }
        public required List<AlunoModel> Alunos { get; set; }
        public required int NumeroDeCreditos { get; set; }

        public CursoModel() { }
    }
}
