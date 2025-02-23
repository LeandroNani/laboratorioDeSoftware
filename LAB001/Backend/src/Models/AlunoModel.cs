using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    [Table("aluno")]
    public class AlunoModel : PessoaModel
    {
        public required string Id { get; set; }
        public required CursoModel Curso { get; set; }
        public required string Matricula { get; set; }
        public required List<DisciplinaModel> DisciplinasFeitas { get; set; }
        public required string Email { get; set; }
        public required int Mensalidade { get; set; }

        public AlunoModel()
            : base() { }
    }
}
