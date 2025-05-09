using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    [Table("disciplina")]
    public class DisciplinaModel
    {
        [Key]
        public required string Id { get; set; } = new Random().Next(100000, 999999).ToString();
        public required string Nome { get; set; }
        public required bool IsActive { get; set; } = false;
        public string ProfessorId { get; set; } = null!;
        public required ProfessorModel Professor { get; set; }
        public required int Preco { get; set; }
        public required string Periodo { get; set; }
        public required List<string>? DisciplinasNecessarias { get; set; } = [];
        public required string Campus { get; set; }
        public required bool Optativa { get; set; }
        public string? Descricao { get; set; }
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
