using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sme.src.Models.Empresa
{
    [Table("produto")]
    public class Produto
    {
        [Key]
        [Column("produto_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("foto", TypeName = "bytea")]
        public required byte[] Foto { get; set; }

        [Column("descricao")]
        public required string Descricao { get; set; }

        [Column("preco")]
        public required int Preco { get; set; }

        [Column("empresa_id"), ForeignKey("empresa_id")]
        public required EmpresaParceira Empresa { get; set; }

        [Column("quantidade")]
        public int Quantidade { get; set; } = 0;
    }
}