using Backend.src.models;
using Backend.src.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.src.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<AlunoModel> Alunos { get; set; }
        public DbSet<ProfessorModel> Professores { get; set; }
        public DbSet<DisciplinaModel> Disciplinas { get; set; }
        public DbSet<CursoModel> Cursos { get; set; }
        public DbSet<CurriculoModel> Curriculos { get; set; }
        public DbSet<PessoaModel> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<AlunoDisciplina>()
                .HasKey(ad => new { ad.AlunoId, ad.DisciplinaId });
        }
    }
}
