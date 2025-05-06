using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sme.src.Models
{
    [Table("professor")]
    public class Professor
    {
        [Key]
        [Column("professor_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("nome")]
        public required string Nome { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        [Column("senha")]
        public required string Senha { get; set; }

        [Column("cpf")]
        public required string Cpf { get; set; }

        [Column("departamento_id"), ForeignKey("departamento_id")]
        public required Departamento Departamento { get; set; }

        [Column("moedas")]
        public int Moedas { get; set; }
    }
}