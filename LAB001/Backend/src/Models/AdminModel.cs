using Backend.src.models.role;

namespace Backend.src.models
{
    public class AdminModel : PessoaModel
    {
        public string Type = RoleExtensions.ToString(Role.ADMIN);
    }
}
