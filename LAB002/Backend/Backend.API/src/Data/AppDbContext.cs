using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Backend.API.Model;

namespace Backend.API.Data
{

    public class AppDbContext : DbContext
    {

        public static readonly JsonSerializerOptions CachedJsonSerializerOptions = new()
        {
        // Exemplo de configurações:
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true, // para formatar JSON com quebras de linha
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        // DbSets
        public DbSet<Usuario> Usuarios { get; set; }  // base para TPH
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Agente> Agentes { get; set; }
        public DbSet<Automovel> Automoveis { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        // Aqui você pode configurar mapeamento adicional via Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Exemplo de Fluent API:
            // modelBuilder.Entity<Cliente>()
            //    .Property(c => c.CPF)
            //    .HasMaxLength(11)
            //    .IsRequired();

            // Configuração do mapeamento TPH (Table Per Hierarchy)
            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Admin>("ADMIN")
                .HasValue<Cliente>("CLIENTE")
                .HasValue<Agente>("AGENTE");

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Rendimentos)
                .HasConversion(
                    static v => JsonSerializer.Serialize(v, CachedJsonSerializerOptions),
                    v => JsonSerializer.Deserialize<List<Rendimento>>(v, CachedJsonSerializerOptions)
                         ?? new List<Rendimento>()
                )
                .HasColumnType("jsonb")
                .Metadata.SetValueComparer(
                    new ValueComparer<List<Rendimento>>(
                        (c1, c2) => JsonSerializer.Serialize(c1, CachedJsonSerializerOptions) ==
                                    JsonSerializer.Serialize(c2, CachedJsonSerializerOptions),
                        c => JsonSerializer.Serialize(c, CachedJsonSerializerOptions).GetHashCode(),
                        c => JsonSerializer.Deserialize<List<Rendimento>>(
                            JsonSerializer.Serialize(c, CachedJsonSerializerOptions),
                            CachedJsonSerializerOptions) ?? new List<Rendimento>()
                    )
                );

            // forçar a coluna a ser do tipo "jsonb" no postgres
            modelBuilder.Entity<Cliente>()
                .Property(c => c.Rendimentos)
                .HasColumnType("jsonb");

            modelBuilder.Entity<Pedido>()
                .Property(p => p.Status)
                .HasDefaultValue(false);
        }
    }
}
