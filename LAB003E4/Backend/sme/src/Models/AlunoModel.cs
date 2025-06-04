using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sme.src.Models.Abstract;

namespace sme.src.Models
{
    [Table("aluno")]
    public class Aluno : Usuario
    {
        [Column("rg")]
        public required string Rg { get; set; }

        [Column("cpf")]
        public required string Cpf { get; set; }

        [Column("instituicao_id")]
        public int InstituicaoId { get; set; }

        [ForeignKey(nameof(InstituicaoId))]
        public required InstituicaoEnsino Instituicao { get; set; }

        [Column("curso_id")]
        public int CursoId { get; set; }

        [ForeignKey(nameof(CursoId))]
        public required Curso Curso { get; set; }

        [Column("moedas")]
        public decimal Moedas { get; set; }
    }
}