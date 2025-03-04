using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    [Table("disciplina")]
    public class DisciplinaModel
    {
        [Key]
        public required string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Nome { get; set; }
        public required bool IsActive { get; set; } = false;

        [ForeignKey("professor_id")]
        public required ProfessorModel Professor { get; set; }
        public required int Preco { get; set; }
        public required string Periodo { get; set; }

        [ForeignKey("disciplina_necessaria_id")]
        public required List<DisciplinaModel> DisciplinasNecessarias { get; set; } = [];
        public required string Campus { get; set; }
        public required bool Optativa { get; set; }
        private int _quantAlunos = 0;
        public required int QuantAlunos
        {
            get => _quantAlunos;
            set
            {
                if (value > 60)
                {
                    throw new InvalidOperationException(
                        "Uma disciplina pode ter, no máximo, 60 alunos"
                    );
                }
                _quantAlunos = value;
            }
        }
    }
}
