using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    [Table("matricula")]
    public class MatriculaModel
    {
        [Key]
        public required string? NumeroDeMatricula { get; set; } =
            new Random().Next(100000, 999999).ToString();
        public required bool Ativa { get; set; }
        public required List<DisciplinaModel> PlanoDeEnsino { get; set; }
        public required int Mensalidade { get; set; }

        public required bool Paga { get; set; }
    }
}
