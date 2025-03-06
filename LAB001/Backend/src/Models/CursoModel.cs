using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    [Table("curso")]
    public class CursoModel
    {
        [Key]
        public required string Id { get; set; } = new Random().Next(100000, 999999).ToString();
        public required string Nome { get; set; }
        public List<DisciplinaModel> Disciplinas { get; set; } = new List<DisciplinaModel>();
        public required int NumeroDeCreditos { get; set; }
    }
}
