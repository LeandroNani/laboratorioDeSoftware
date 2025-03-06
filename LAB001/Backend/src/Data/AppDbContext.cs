using System.Collections.Generic;
using System.Text.Json;
using Backend.src.models;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public static readonly JsonSerializerOptions CachedJsonSerializerOptions = new();

        public DbSet<AlunoModel> Alunos { get; set; }
        public DbSet<ProfessorModel> Professores { get; set; }
        public DbSet<DisciplinaModel> Disciplinas { get; set; }
        public DbSet<CursoModel> Cursos { get; set; }
        public DbSet<CurriculoModel> Curriculos { get; set; }
        public DbSet<PessoaModel> Pessoas { get; set; }
        public DbSet<AdminModel> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder
                .Entity<DisciplinaModel>()
                .Property(d => d.DisciplinasNecessarias)
                .HasConversion(
                    static v =>
                        JsonSerializer.Serialize(v, AppDbContext.CachedJsonSerializerOptions),
                    v =>
                        JsonSerializer.Deserialize<List<string>>(
                            v,
                            AppDbContext.CachedJsonSerializerOptions
                        ) ?? new List<string>()
                );
        }
    }
}
