using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sme.src.Models.Empresa;

namespace sme.src.Models
{
    [Table("transacao")]
    public abstract class Transacao
    {
        [Key]
        [Column("transacao_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("aluno_id"), ForeignKey("aluno_id")]
        public required Aluno Aluno { get; set; }
        
        [Column("motivo")]
        public required string Motivo { get; set; }

        [Column("valor")]
        public required decimal Valor { get; set; }

        [Column("data_transacao")]
        public DateTime DataTransacao { get; set; } = DateTime.Now;
    }

    [Table("transacao_professor_aluno")]
    public class TransacaoProfessor : Transacao
    {
        [Column("professor_id"), ForeignKey("professor_id")]
        public required Professor Professor { get; set; }
    }

    [Table("transacao_aluno_empresa")]
    public class TransacaoEmpresa : Transacao
    {   
        [Column("empresa_id"), ForeignKey("empresa_id")]
        public required EmpresaParceira EmpresaParceira { get; set; }

        [Column("produto_id"), ForeignKey("produto_id")]
        public required ICollection<Produto> Produtos { get; set; }
    }
}