using System.Collections.Generic;

namespace Backend.API.Model
{
    public class Cliente : Usuario
    {
        public override Role Role  => Role.CLIENTE;

        
        public required string RG { get; set; }
        public required string CPF { get; set; }
        public required string Profissao { get; set; }
        public required string EntidadeEmpregadora { get; set; }
        
        public List<Rendimento> Rendimentos { get; set; } = new();
        public ICollection<Pedido>? Pedidos { get; set; } = new List<Pedido>();
    }
}
