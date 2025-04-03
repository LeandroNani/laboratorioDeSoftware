namespace Backend.API.Model
{
    public class Pedido
    {
        public int Id { get; set; }

        // Ex.: "pendente", "aprovado", "negado", "cancelado"
        public string Status { get; set; } = "pendente";

        // Duração do aluguel (ex: em dias)
        public int Duracao { get; set; }

        // "credito" ou "debito"
        public string? TipoContrato { get; set; }

        // Relação com Cliente (Contratante)
        public int ContratanteId { get; set; }
        public Cliente? Contratante { get; set; }

        // Relação com Agente designado
        public int AgenteDesignadoId { get; set; }
        public Agente? AgenteDesignado { get; set; }

        // Relação com Automovel
        public int AutomovelId { get; set; }
        public Automovel? Automovel { get; set; }
    }
}
