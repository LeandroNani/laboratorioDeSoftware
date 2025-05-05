using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using sme.src.Models;
using sme.src.Models.Empresa;
using sme.src.Models.Relations;

namespace sme.src.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Produto> Produtos { get; set; } = null!;
        public DbSet<Professor> Professores { get; set; } = null!;
        public DbSet<Aluno> Alunos { get; set; } = null!;
        public DbSet<Curso> Cursos { get; set; } = null!;
        public DbSet<InstituicaoEnsino> Instituicoes { get; set; } = null!;
        public DbSet<Departamento> Departamentos { get; set; } = null!;
        public DbSet<EmpresaParceira> EmpresasParceiras { get; set; } = null!;
        public DbSet<TransacaoProfessor> TransacoesProfessor { get; set; } = null!;
        public DbSet<TransacaoEmpresa> TransacoesEmpresa { get; set; } = null!;
        public DbSet<ProfessorDepartamento> ProfessorDepartamentos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var DatabaseName  = configuration["Database:Nome"] ?? throw new Exception("Nome do banco de dados não está no appsettings.json");
            var DatabaseSenha = configuration["Database:Senha"] ?? throw new Exception("Nome do banco de dados não está no appsettings.json");
            var Port          = int.Parse(configuration["Database:Port"] ?? throw new Exception("Nome do banco de dados não está no appsettings.json"));
            optionsBuilder.UseNpgsql($"Host=localhost;Port={Port};Database={DatabaseName};Username=postgres;Password={DatabaseSenha}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .Property(p => p.ProdutoId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Professor>()
                .Property(p => p.ProfessorId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Aluno>()
                .Property(a => a.AlunoId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Curso>()
                .Property(c => c.CursoId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<InstituicaoEnsino>()
                .Property(i => i.InstituicaoId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Departamento>()
                .Property(d => d.DepartamentoId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<EmpresaParceira>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
