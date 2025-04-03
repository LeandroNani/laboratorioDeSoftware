namespace Backend.API.Model
{
    public class Automovel
    {
        public int Id { get; set; }             // PK
        public required int Matricula { get; set; }
        public required int Ano { get; set; }
        public required string Marca { get; set; }
        public required string Modelo { get; set; }
        public required string Placa { get; set; }

        public string? FotoUrl { get; set; }

        // Relação com Pedido, se 1:1 ou 1:N depende do modelo
        public ICollection<Pedido>? Pedidos { get; set; } = new List<Pedido>();

        // Relação com Agente (1..*)
        public int? AgenteId { get; set; }
        public Agente? Agente { get; set; }

        public int? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
