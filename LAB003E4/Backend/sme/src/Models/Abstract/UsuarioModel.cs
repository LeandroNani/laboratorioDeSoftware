using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace sme.src.Models.Abstract
{
    [Table("usuario")]
    [Index(nameof(Email), IsUnique = true)]
    public abstract class Usuario
    {
        [Key]
        [Column("usuario_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("nome")]
        public required string Nome { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        [Column("senha")]
        public required string Senha { get; set; }
    }
}