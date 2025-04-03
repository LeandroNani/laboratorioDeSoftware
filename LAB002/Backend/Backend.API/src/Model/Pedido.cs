namespace Backend.API.Model
{
    public class Pedido
    {
        public int Id { get; set; }
        public required string Status { get; set; }  = "pendente";  // Ex.: true = ativo, false = cancelado
        
        public int duracao;

        
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
