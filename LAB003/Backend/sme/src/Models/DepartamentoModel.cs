using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sme.src.Models
{
    [Table("departamento")]
    public class Departamento 
    {
        [Key]
        [Column("departamento_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartamentoId { get; set; }

        [Column("nome")]
        public required string Nome { get; set; }

        [Column("instituicao_id"), ForeignKey("instituicao_id")]
        public required InstituicaoEnsino Instituicao { get; set; }

        [Column("curso_id"), ForeignKey("curso_id")]
        public required Curso Curso { get; set; }
    }
}