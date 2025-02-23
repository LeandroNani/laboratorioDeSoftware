using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.models
{
    [Table("pessoa")]
    public abstract class PessoaModel
    {
        public required int NumeroDePessoa { get; set; }
        public required string Nome { get; set; }
        public required string Senha { get; set; }

        public PessoaModel() { }
    }
}
