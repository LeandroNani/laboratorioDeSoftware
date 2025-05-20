using System.ComponentModel.DataAnnotations.Schema;
using Backend.src.models.role;

namespace Backend.src.models
{
    [Table("professor")]
    public class ProfessorModel : PessoaModel
    {
        public required string NivelEscolar { get; set; }
        public required string Email { get; set; }
        public string Type = RoleExtensions.ToString(Role.PROFESSOR);
    }
}
