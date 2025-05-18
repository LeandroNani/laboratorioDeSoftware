using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sme.src.Models.Empresa
{
    [Table("empresa_parceira")]
    public class EmpresaParceira
    {
        [Key]
        [Column("empresa_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("nome")]
        public required string Nome { get; set; }
        
        [Column("senha")]
        public required string Senha { get; set; }

        [Column("email")]
        public required string Email { get; set; }

    }
}