namespace Backend.API.Model 
{
    public class Contrato
    {
        public int Id { get; set; }

        // Data de inicio e fim
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        // Tipo do contrato (ex: "credito", "debito"), puxado do Pedido
        public string TipoContrato { get; set; } = string.Empty;

        // Relações
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int AgenteId { get; set; }
        public Agente? Agente { get; set; }

        // Se quiser relacionar com Pedido
        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
    }

}
