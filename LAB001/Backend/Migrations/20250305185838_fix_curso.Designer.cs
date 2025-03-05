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
    [Migration("20250305185838_fix_curso")]
    partial class fix_curso
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Backend.src.models.AlunoDisciplina", b =>
                {
                    b.Property<string>("AlunoId")
                        .HasColumnType("text");

                    b.Property<string>("DisciplinaId")
                        .HasColumnType("text");

                    b.Property<string>("aluno_id")
                        .IsRequired()
                        .HasColumnType("text");

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

                    b.Property<string>("AlunoModelNumeroDePessoa")
                        .HasColumnType("text");

                    b.Property<string>("Campus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Descricao")
                        .HasColumnType("text");

                    b.Property<string>("DisciplinaModelId")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("MatriculaModelNumeroDeMatricula")
                        .HasColumnType("text");

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

                    b.Property<string>("ProfessorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("QuantAlunos")
                        .HasColumnType("integer");

                    b.Property<string>("disciplina_id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AlunoModelNumeroDePessoa");

                    b.HasIndex("DisciplinaModelId");

                    b.HasIndex("MatriculaModelNumeroDeMatricula");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("disciplina_id");

                    b.ToTable("disciplina");
                });

            modelBuilder.Entity("Backend.src.models.MatriculaModel", b =>
                {
                    b.Property<string>("NumeroDeMatricula")
                        .HasColumnType("text");

                    b.Property<bool>("Ativa")
                        .HasColumnType("boolean");

                    b.Property<int>("Mensalidade")
                        .HasColumnType("integer");

                    b.HasKey("NumeroDeMatricula");

                    b.ToTable("matricula");
                });

            modelBuilder.Entity("Backend.src.models.PessoaModel", b =>
                {
                    b.Property<string>("NumeroDePessoa")
                        .HasColumnType("text");

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

                    b.Property<string>("CursoId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MatriculaId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("aluno_id")
                        .HasColumnType("text");

                    b.HasIndex("CursoId");

                    b.HasIndex("MatriculaId");

                    b.HasIndex("aluno_id");

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

            modelBuilder.Entity("Backend.src.models.AlunoDisciplina", b =>
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
                        .WithMany("Cursos")
                        .HasForeignKey("CurriculoModelId");
                });

            modelBuilder.Entity("Backend.src.models.DisciplinaModel", b =>
                {
                    b.HasOne("Backend.src.models.AlunoModel", null)
                        .WithMany("DisciplinasCursadas")
                        .HasForeignKey("AlunoModelNumeroDePessoa");

                    b.HasOne("Backend.src.models.DisciplinaModel", null)
                        .WithMany("DisciplinasNecessarias")
                        .HasForeignKey("DisciplinaModelId");

                    b.HasOne("Backend.src.models.MatriculaModel", null)
                        .WithMany("PlanoDeEnsino")
                        .HasForeignKey("MatriculaModelNumeroDeMatricula");

                    b.HasOne("Backend.src.models.ProfessorModel", "Professor")
                        .WithMany("Disciplinas")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.src.models.CurriculoModel", null)
                        .WithMany("Disciplinas")
                        .HasForeignKey("disciplina_id");

                    b.HasOne("Backend.src.models.CursoModel", null)
                        .WithMany("Disciplinas")
                        .HasForeignKey("disciplina_id");

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
                    b.HasOne("Backend.src.models.CursoModel", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId");

                    b.HasOne("Backend.src.models.MatriculaModel", "Matricula")
                        .WithMany()
                        .HasForeignKey("MatriculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.src.models.PessoaModel", null)
                        .WithOne()
                        .HasForeignKey("Backend.src.models.AlunoModel", "NumeroDePessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.src.models.CurriculoModel", null)
                        .WithMany("Alunos")
                        .HasForeignKey("aluno_id");

                    b.Navigation("Curso");

                    b.Navigation("Matricula");
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

                    b.Navigation("Cursos");

                    b.Navigation("Disciplinas");

                    b.Navigation("Professores");
                });

            modelBuilder.Entity("Backend.src.models.CursoModel", b =>
                {
                    b.Navigation("Disciplinas");
                });

            modelBuilder.Entity("Backend.src.models.DisciplinaModel", b =>
                {
                    b.Navigation("DisciplinasNecessarias");
                });

            modelBuilder.Entity("Backend.src.models.MatriculaModel", b =>
                {
                    b.Navigation("PlanoDeEnsino");
                });

            modelBuilder.Entity("Backend.src.models.AlunoModel", b =>
                {
                    b.Navigation("DisciplinasCursadas");
                });

            modelBuilder.Entity("Backend.src.models.ProfessorModel", b =>
                {
                    b.Navigation("Disciplinas");
                });
#pragma warning restore 612, 618
        }
    }
}
