namespace Backend.src.models.role
{
    public enum Role
    {
        ADMIN = 1,
        STUDENT = 2,
        PROFESSOR = 3,
    }

    public static class RoleExtensions
    {
        public static string ToString(this Role role)
        {
            return role switch
            {
                Role.ADMIN => "ADMIN",
                Role.STUDENT => "STUDENT",
                Role.PROFESSOR => "PROFESSOR",
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null),
            };
        }
    }
}
