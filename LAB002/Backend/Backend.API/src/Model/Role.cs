namespace Backend.API.Model
{
    public enum Role
    {
        ADMIN = 1,
        CLIENTE = 2,
        AGENTE = 3
    }

    public static class RoleExtensions
    {
        // Método de extensão que retorna a string correspondente ao enum
        public static string ToString(this Role role)
        {
            return role switch
            {
                Role.ADMIN => "ADMIN",
                Role.CLIENTE => "CLIENTE",
                Role.AGENTE => "AGENTE",
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null),
            };
        }
    }
}