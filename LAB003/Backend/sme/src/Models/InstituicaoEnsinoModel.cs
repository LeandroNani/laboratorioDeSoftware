using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sme.src.Models
{
    [Table("instituicao")]
    public class InstituicaoEnsino 
    {
        [Key]
        [Column("instituicao_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstituicaoId { get; set; }

        [Column("nome")]
        public required string Nome { get; set; }
    }
}