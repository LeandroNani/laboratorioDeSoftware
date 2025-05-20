using System.ComponentModel.DataAnnotations.Schema;
using Backend.src.models.role;

namespace Backend.src.models
{
    [Table("pessoa_admin")]
    public class AdminModel : PessoaModel
    {
        public string Type = RoleExtensions.ToString(Role.ADMIN);
    }
}
