using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sme.src.Models.Abstract;

namespace sme.src.Models.Empresa
{
    [Table("empresa_parceira")]
    public class EmpresaParceira : Usuario
    {
        [Column("cnpj")]
        [Required(ErrorMessage = "CNPJ é obrigatório.")]
        [StringLength(14, ErrorMessage = "CNPJ deve ter 14 caracteres.")]
        public required string Cnpj { get; set; }

        [Column("razao_social")]
        [StringLength(100, ErrorMessage = "Razão Social deve ter no máximo 100 caracteres.")]
        public string? RazaoSocial { get; set; }

        [Column("nome_fantasia")]
        [StringLength(100, ErrorMessage = "Nome Fantasia deve ter no máximo 100 caracteres.")]
        public string? NomeFantasia { get; set; }
    }
}