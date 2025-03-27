namespace Backend.API.Models
{
    public class Agente
    {
        public int Id { get; set; }               // PK
        public required string CNPJ { get; set; }
        public required string Nome { get; set; }
        public string? Endereco { get; set; }
        public required int QuantidadeCarros { get; set; }

        // Relação com Pedidos
        public ICollection<Pedido>? PedidosDesignados { get; set; } = new List<Pedido>();

        public ICollection<Automovel> Automoveis { get; set; } 
    }
}
