using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sme.src.Models.Abstract;

namespace sme.src.Models
{
    [Table("professor")]
    public class Professor : Usuario
    {
        [Column("cpf")]
        public required string Cpf { get; set; }

        [Column("departamento_id")]
        public int DepartamentoId { get; set; }

        [ForeignKey(nameof(DepartamentoId))]
        public required Departamento Departamento { get; set; }

        [Column("moedas")]
        public int Moedas { get; set; }

        // Data da última alocação semestral
        [Column("last_allocation_date")]
        public DateTime LastAllocationDate { get; set; } = DateTime.UtcNow;
    }
}