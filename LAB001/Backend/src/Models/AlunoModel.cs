using System.ComponentModel.DataAnnotations.Schema;
using Backend.src.models.role;

namespace Backend.src.models
{
    [Table("aluno")]
    public class AlunoModel : PessoaModel
    {
        [ForeignKey("curso_id")]
        public CursoModel? Curso { get; set; }

        [ForeignKey("numero_matricula")]
        public required MatriculaModel Matricula { get; set; }

        [ForeignKey("disciplina_cursada_id")]
        public List<DisciplinaModel>? DisciplinasCursadas { get; set; } = [];
        public required string Email { get; set; }
        public string Type { get; set; } = RoleExtensions.ToString(Role.STUDENT);
    }
}
