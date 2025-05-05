using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sme.src.Models.Relations
{
    [Table("professor_departamento")]
    public class ProfessorDepartamento
    {
        [Key]
        [Column("professor_departamento_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("professor_id"), ForeignKey("professor_id")]
        public required Professor Professor { get; set; }

        [Column("departamento_id"), ForeignKey("departamento_id")]
        public required Departamento Departamento { get; set; }
    }
}