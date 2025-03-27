namespace Backend.API.Models
{
    public class Cliente
    {
        public int Id { get; set; }              // Primary key (gerada automaticamente pelo EF)
        public required string RG { get; set; }
        public required string CPF { get; set; }
        public required string Nome { get; set; }
        public required string Endereco { get; set; }
        public required string Profissao { get; set; }
        public required string EntidadeEmpregadora { get; set; }
        
        public List<Rendimento> Rendimentos { get; set; } = new();
        public ICollection<Pedido>? Pedidos { get; set; } = new List<Pedido>();
    }
}
