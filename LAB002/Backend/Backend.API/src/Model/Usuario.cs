using System.ComponentModel.DataAnnotations;

namespace Backend.API.Model
{
    public abstract class Usuario
    {
        [Key]
        public int Id { get; set; }

        public required string Nome { get; set; }

        public string? Endereco { get; set; }
        public required string Email { get; set; }
        public required string SenhaHash { get; set; }

        // Cada subclasse vai sobrescrever e retornar seu papel
        public abstract Role Role { get; }
    }
}