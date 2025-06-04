using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using sme.src.Models;
using sme.src.Models.Abstract;
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
        public DbSet<TransacaoProfessor> TransacoesProfessorAluno { get; set; } = null!;
        public DbSet<TransacaoEmpresa> TransacoesEmpresa { get; set; } = null!;
        public DbSet<ProfessorDepartamento> ProfessorDepartamentos { get; set; } = null!;
        public DbSet<Transacao> Transacoes { get; set; } = null!;
        public DbSet<TransacaoEmpresa> TransacoesAlunoEmpresa { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var DatabaseName = Environment.GetEnvironmentVariable("DB_NAME") ??
                configuration["Database:Nome"] ?? throw new Exception("Nome do banco de dados não está no appsettings.json");
            var DatabaseSenha = Environment.GetEnvironmentVariable("DB_PASSWORD") ??
                configuration["Database:Senha"] ?? throw new Exception("Senha do banco de dados não está no appsettings.json");
            var Port = Environment.GetEnvironmentVariable("DB_PORT") ??
                configuration["Database:Port"] ?? throw new Exception("Porta do banco de dados não está no appsettings.json");
            optionsBuilder.UseNpgsql($"Host=localhost;Port={Port};Database={DatabaseName};Username=postgres;Password={DatabaseSenha}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
