using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sme.src.Models
{
    [Table("curso")]
    public class Curso
    {
        [Key]
        [Column("curso_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("nome")]
        public required string Nome { get; set; }
    }
}