using System.Collections.Generic;

namespace Backend.API.Model
{
    public class Agente : Usuario
    {
        public override Role Role => Role.AGENTE;
        public required string CNPJ { get; set; }
        public required int QuantidadeCarros { get; set; }

        // Relação com Pedidos
        public ICollection<Pedido>? PedidosDesignados { get; set; } = new List<Pedido>();

        public ICollection<Automovel> Automoveis { get; set; }  = new List<Automovel>();
    }
}
