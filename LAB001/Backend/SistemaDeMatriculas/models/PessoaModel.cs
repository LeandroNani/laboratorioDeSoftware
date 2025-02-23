namespace SistemaDeMatriculas.Models
{
    public abstract class PessoaModel
    {
        public required int NumeroDePessoa { get; set; }
        public required string Nome { get; set; }
        public required string Senha { get; set; }

        public PessoaModel()
        {
        }
    }
}