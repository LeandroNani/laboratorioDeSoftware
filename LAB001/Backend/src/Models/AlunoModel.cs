using System.ComponentModel.DataAnnotations.Schema;
using Backend.src.models.role;

namespace Backend.src.models
{
    [Table("aluno")]
    public class AlunoModel : PessoaModel
    {
        public string MatriculaId { get; set; } = null!;

        [ForeignKey("MatriculaId")]
        public required MatriculaModel Matricula { get; set; }
        public required CursoModel Curso { get; set; }
        public List<DisciplinaModel>? DisciplinasCursadas { get; set; } = [];
        public required string Email { get; set; }
        public string Type { get; set; } = RoleExtensions.ToString(Role.STUDENT);
    }
}
