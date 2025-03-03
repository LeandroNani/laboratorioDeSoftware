using System.ComponentModel.DataAnnotations;

namespace Backend.src.models
{
    public abstract class PessoaModel
    {
        [Key]
        public required int NumeroDePessoa { get; set; }
        public required string Nome { get; set; }
        public required string Senha { get; set; }
    }
}
