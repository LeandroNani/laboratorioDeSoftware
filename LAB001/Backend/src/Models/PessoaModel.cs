using System.ComponentModel.DataAnnotations;

namespace Backend.src.models
{
    public abstract class PessoaModel
    {
        [Key]
        public int NumeroDePessoa { get; set; } = new Random().Next(100000, 999999);
        public required string Nome { get; set; }
        public required string Senha { get; set; }
    }
}
