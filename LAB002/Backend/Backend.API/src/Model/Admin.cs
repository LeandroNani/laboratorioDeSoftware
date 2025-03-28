namespace Backend.API.Model
{
    public class Admin : Usuario
    {
        public override Role Role => Role.ADMIN;

    }
}