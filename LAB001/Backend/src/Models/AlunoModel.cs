using System.ComponentModel.DataAnnotations.Schema;
using Backend.src.models.role;

namespace Backend.src.models
{
    [Table("aluno")]
    public class AlunoModel : PessoaModel
    {
        [ForeignKey("curso_id")]
        public CursoModel? Curso { get; set; }
        public required string Matricula { get; set; }

        [ForeignKey("disciplina_cursada_id")]
        public List<DisciplinaModel>? DisciplinasCursadas { get; set; }

        [ForeignKey("plano_de_ensino_id")]
        private List<DisciplinaModel>? planoDeEnsino;
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

        public required string Email { get; set; }
        public required int Mensalidade { get; set; }
        public string Type { get; set; } = RoleExtensions.ToString(Role.STUDENT);
    }
}
