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

        [ForeignKey("plano_de_ensino_id")]
        private List<DisciplinaModel>? planoDeEnsino = [];
        public required List<DisciplinaModel>? PlanoDeEnsino // Disciplinas que o aluno está cursando atualmene
        {
            get => planoDeEnsino;
            set
            {
                if (value != null && value.Count > 6)
                {
                    throw new InvalidOperationException(
                        "O aluno não pode se matricular em mais do que 6 disciplinas"
                    );
                }
                planoDeEnsino = value;
            }
        }
        public required int Mensalidade { get; set; }
    }
}
