namespace Backend.API.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public required bool Status { get; set; }    // Ex.: true = ativo, false = cancelado
        
        // Chaves estrangeiras
        public required int ContratanteId { get; set; }
        public required Cliente Contratante { get; set; }

        public required int AgenteDesignadoId { get; set; }
        public required Agente AgenteDesignado { get; set; }

        public required int AutomovelId { get; set; }
        public required Automovel Automovel { get; set; }

        // Data de criação, data de modificação etc. se necessário
    }
}
