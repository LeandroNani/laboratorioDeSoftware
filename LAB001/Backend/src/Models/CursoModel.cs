using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    [Table("curso")]
    public class CursoModel
    {
        [Key]
        public required string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Nome { get; set; }

        [ForeignKey("disciplina_id")]
        public required List<DisciplinaModel> Disciplinas { get; set; } = [];
        public required List<AlunoModel> Alunos { get; set; } = [];
        public required int NumeroDeCreditos { get; set; }
    }
}
