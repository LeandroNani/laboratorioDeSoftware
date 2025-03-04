﻿// <auto-generated />
using Backend.src.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Backend.src.Models.AlunoDisciplina", b =>
                {
                    b.Property<int>("AlunoId")
                        .HasColumnType("integer");

                    b.Property<int>("DisciplinaId")
                        .HasColumnType("integer");

                    b.Property<int>("aluno_id")
                        .HasColumnType("integer");

                    b.Property<string>("disciplina_id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AlunoId", "DisciplinaId");

                    b.HasIndex("aluno_id");

                    b.HasIndex("disciplina_id");

                    b.ToTable("aluno_disciplina");
                });

            modelBuilder.Entity("Backend.src.models.CurriculoModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Semestre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("curriculo");
                });

            modelBuilder.Entity("Backend.src.models.CursoModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumeroDeCreditos")
                        .HasColumnType("integer");

                    b.Property<string>("curso_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("curso_id");

                    b.ToTable("curso");
                });

            modelBuilder.Entity("Backend.src.models.DisciplinaModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int?>("AlunoModelNumeroDePessoa")
                        .HasColumnType("integer");

                    b.Property<string>("Campus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Optativa")
                        .HasColumnType("boolean");

                    b.Property<string>("Periodo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Preco")
                        .HasColumnType("integer");

                    b.Property<int>("QuantAlunos")
                        .HasColumnType("integer");

                    b.Property<int?>("disciplina_cursada_id")
                        .HasColumnType("integer");

                    b.Property<string>("disciplina_id")
                        .HasColumnType("text");

                    b.Property<string>("disciplina_necessaria_id")
                        .HasColumnType("text");

                    b.Property<int>("professor_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AlunoModelNumeroDePessoa");

                    b.HasIndex("disciplina_cursada_id");

                    b.HasIndex("disciplina_id");

                    b.HasIndex("disciplina_necessaria_id");

                    b.HasIndex("professor_id");

                    b.ToTable("disciplina");
                });

            modelBuilder.Entity("Backend.src.models.PessoaModel", b =>
                {
                    b.Property<int>("NumeroDePessoa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("NumeroDePessoa"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("NumeroDePessoa");

                    b.ToTable("Pessoas");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Backend.src.models.AdminModel", b =>
                {
                    b.HasBaseType("Backend.src.models.PessoaModel");

                    b.ToTable("pessoa_admin");
                });

            modelBuilder.Entity("Backend.src.models.AlunoModel", b =>
                {
                    b.HasBaseType("Backend.src.models.PessoaModel");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Mensalidade")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("aluno_id")
                        .HasColumnType("text");

                    b.Property<string>("curso_id")
                        .HasColumnType("text");

                    b.HasIndex("aluno_id");

                    b.HasIndex("curso_id");

                    b.ToTable("aluno");
                });

            modelBuilder.Entity("Backend.src.models.ProfessorModel", b =>
                {
                    b.HasBaseType("Backend.src.models.PessoaModel");

                    b.Property<string>("NivelEscolar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("professor_id")
                        .HasColumnType("text");

                    b.HasIndex("professor_id");

                    b.ToTable("professor");
                });

            modelBuilder.Entity("Backend.src.Models.AlunoDisciplina", b =>
                {
                    b.HasOne("Backend.src.models.AlunoModel", "Aluno")
                        .WithMany()
                        .HasForeignKey("aluno_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.src.models.DisciplinaModel", "Disciplina")
                        .WithMany()
                        .HasForeignKey("disciplina_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aluno");

                    b.Navigation("Disciplina");
                });

            modelBuilder.Entity("Backend.src.models.CursoModel", b =>
                {
                    b.HasOne("Backend.src.models.CurriculoModel", null)
                        .WithMany("Curso")
                        .HasForeignKey("curso_id");
                });

            modelBuilder.Entity("Backend.src.models.DisciplinaModel", b =>
                {
                    b.HasOne("Backend.src.models.AlunoModel", null)
                        .WithMany("PlanoDeEnsino")
                        .HasForeignKey("AlunoModelNumeroDePessoa");

                    b.HasOne("Backend.src.models.AlunoModel", null)
                        .WithMany("DisciplinasCursadas")
                        .HasForeignKey("disciplina_cursada_id");

                    b.HasOne("Backend.src.models.CurriculoModel", null)
                        .WithMany("Disciplinas")
                        .HasForeignKey("disciplina_id");

                    b.HasOne("Backend.src.models.CursoModel", null)
                        .WithMany("Disciplinas")
                        .HasForeignKey("disciplina_id");

                    b.HasOne("Backend.src.models.DisciplinaModel", null)
                        .WithMany("DisciplinasNecessarias")
                        .HasForeignKey("disciplina_necessaria_id");

                    b.HasOne("Backend.src.models.ProfessorModel", "Professor")
                        .WithMany("Disciplinas")
                        .HasForeignKey("professor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("Backend.src.models.AdminModel", b =>
                {
                    b.HasOne("Backend.src.models.PessoaModel", null)
                        .WithOne()
                        .HasForeignKey("Backend.src.models.AdminModel", "NumeroDePessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.src.models.AlunoModel", b =>
                {
                    b.HasOne("Backend.src.models.PessoaModel", null)
                        .WithOne()
                        .HasForeignKey("Backend.src.models.AlunoModel", "NumeroDePessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.src.models.CurriculoModel", null)
                        .WithMany("Alunos")
                        .HasForeignKey("aluno_id");

                    b.HasOne("Backend.src.models.CursoModel", "Curso")
                        .WithMany("Alunos")
                        .HasForeignKey("curso_id");

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("Backend.src.models.ProfessorModel", b =>
                {
                    b.HasOne("Backend.src.models.PessoaModel", null)
                        .WithOne()
                        .HasForeignKey("Backend.src.models.ProfessorModel", "NumeroDePessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.src.models.CurriculoModel", null)
                        .WithMany("Professores")
                        .HasForeignKey("professor_id");
                });

            modelBuilder.Entity("Backend.src.models.CurriculoModel", b =>
                {
                    b.Navigation("Alunos");

                    b.Navigation("Curso");

                    b.Navigation("Disciplinas");

                    b.Navigation("Professores");
                });

            modelBuilder.Entity("Backend.src.models.CursoModel", b =>
                {
                    b.Navigation("Alunos");

                    b.Navigation("Disciplinas");
                });

            modelBuilder.Entity("Backend.src.models.DisciplinaModel", b =>
                {
                    b.Navigation("DisciplinasNecessarias");
                });

            modelBuilder.Entity("Backend.src.models.AlunoModel", b =>
                {
                    b.Navigation("DisciplinasCursadas");

                    b.Navigation("PlanoDeEnsino");
                });

            modelBuilder.Entity("Backend.src.models.ProfessorModel", b =>
                {
                    b.Navigation("Disciplinas");
                });
#pragma warning restore 612, 618
        }
    }
}
