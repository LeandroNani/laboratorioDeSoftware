﻿// <auto-generated />
using Backend.src.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250224033416_names")]
    partial class names
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AlunoModelDisciplinaModel", b =>
                {
                    b.Property<int>("AlunosNumeroDePessoa")
                        .HasColumnType("integer");

                    b.Property<string>("DisciplinasCursadasId")
                        .HasColumnType("text");

                    b.HasKey("AlunosNumeroDePessoa", "DisciplinasCursadasId");

                    b.HasIndex("DisciplinasCursadasId");

                    b.ToTable("AlunoModelDisciplinaModel");
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

                    b.Property<string>("CurriculoModelId")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumeroDeCreditos")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CurriculoModelId");

                    b.ToTable("curso");
                });

            modelBuilder.Entity("Backend.src.models.DisciplinaModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Campus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CurriculoModelId")
                        .HasColumnType("text");

                    b.Property<string>("CursoModelId")
                        .HasColumnType("text");

                    b.Property<string>("DisciplinaModelId")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Periodo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Preco")
                        .HasColumnType("integer");

                    b.Property<int>("ProfessorNumeroDePessoa")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CurriculoModelId");

                    b.HasIndex("CursoModelId");

                    b.HasIndex("DisciplinaModelId");

                    b.HasIndex("ProfessorNumeroDePessoa");

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

                    b.ToTable("pessoa");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Backend.src.models.AlunoModel", b =>
                {
                    b.HasBaseType("Backend.src.models.PessoaModel");

                    b.Property<string>("CurriculoModelId")
                        .HasColumnType("text");

                    b.Property<string>("CursoId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Mensalidade")
                        .HasColumnType("integer");

                    b.HasIndex("CurriculoModelId");

                    b.HasIndex("CursoId");

                    b.ToTable("aluno");
                });

            modelBuilder.Entity("Backend.src.models.ProfessorModel", b =>
                {
                    b.HasBaseType("Backend.src.models.PessoaModel");

                    b.Property<string>("CurriculoModelId")
                        .HasColumnType("text");

                    b.Property<string>("NivelEscolar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasIndex("CurriculoModelId");

                    b.ToTable("professor");
                });

            modelBuilder.Entity("AlunoModelDisciplinaModel", b =>
                {
                    b.HasOne("Backend.src.models.AlunoModel", null)
                        .WithMany()
                        .HasForeignKey("AlunosNumeroDePessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.src.models.DisciplinaModel", null)
                        .WithMany()
                        .HasForeignKey("DisciplinasCursadasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.src.models.CursoModel", b =>
                {
                    b.HasOne("Backend.src.models.CurriculoModel", null)
                        .WithMany("Curso")
                        .HasForeignKey("CurriculoModelId");
                });

            modelBuilder.Entity("Backend.src.models.DisciplinaModel", b =>
                {
                    b.HasOne("Backend.src.models.CurriculoModel", null)
                        .WithMany("Disciplinas")
                        .HasForeignKey("CurriculoModelId");

                    b.HasOne("Backend.src.models.CursoModel", null)
                        .WithMany("Disciplinas")
                        .HasForeignKey("CursoModelId");

                    b.HasOne("Backend.src.models.DisciplinaModel", null)
                        .WithMany("DisciplinasNecessarias")
                        .HasForeignKey("DisciplinaModelId");

                    b.HasOne("Backend.src.models.ProfessorModel", "Professor")
                        .WithMany("Disciplinas")
                        .HasForeignKey("ProfessorNumeroDePessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("Backend.src.models.AlunoModel", b =>
                {
                    b.HasOne("Backend.src.models.CurriculoModel", null)
                        .WithMany("Alunos")
                        .HasForeignKey("CurriculoModelId");

                    b.HasOne("Backend.src.models.CursoModel", "Curso")
                        .WithMany("Alunos")
                        .HasForeignKey("CursoId");

                    b.HasOne("Backend.src.models.PessoaModel", null)
                        .WithOne()
                        .HasForeignKey("Backend.src.models.AlunoModel", "NumeroDePessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("Backend.src.models.ProfessorModel", b =>
                {
                    b.HasOne("Backend.src.models.CurriculoModel", null)
                        .WithMany("Professores")
                        .HasForeignKey("CurriculoModelId");

                    b.HasOne("Backend.src.models.PessoaModel", null)
                        .WithOne()
                        .HasForeignKey("Backend.src.models.ProfessorModel", "NumeroDePessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("Backend.src.models.ProfessorModel", b =>
                {
                    b.Navigation("Disciplinas");
                });
#pragma warning restore 612, 618
        }
    }
}
