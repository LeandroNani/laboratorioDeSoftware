﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using sme.src.Data;

#nullable disable

namespace sme.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250505034913_new")]
    partial class @new
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("sme.src.Models.Aluno", b =>
                {
                    b.Property<int>("AlunoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("aluno_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AlunoId"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cpf");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<int>("Moedas")
                        .HasColumnType("integer")
                        .HasColumnName("moedas");

                    b.Property<string>("Rg")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("rg");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("senha");

                    b.Property<int>("curso_id")
                        .HasColumnType("integer");

                    b.Property<int>("instituicao_id")
                        .HasColumnType("integer");

                    b.HasKey("AlunoId");

                    b.HasIndex("curso_id");

                    b.HasIndex("instituicao_id");

                    b.ToTable("aluno");
                });

            modelBuilder.Entity("sme.src.Models.Curso", b =>
                {
                    b.Property<int>("CursoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("curso_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CursoId"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.HasKey("CursoId");

                    b.ToTable("curso");
                });

            modelBuilder.Entity("sme.src.Models.Departamento", b =>
                {
                    b.Property<int>("DepartamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("departamento_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DepartamentoId"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<int>("curso_id")
                        .HasColumnType("integer");

                    b.Property<int>("instituicao_id")
                        .HasColumnType("integer");

                    b.HasKey("DepartamentoId");

                    b.HasIndex("curso_id");

                    b.HasIndex("instituicao_id");

                    b.ToTable("departamento");
                });

            modelBuilder.Entity("sme.src.Models.Empresa.EmpresaParceira", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("empresa_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("senha");

                    b.HasKey("Id");

                    b.ToTable("empresa_parceira");
                });

            modelBuilder.Entity("sme.src.Models.Empresa.Produto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("produto_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProdutoId"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("descricao");

                    b.Property<byte[]>("Foto")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("foto");

                    b.Property<int>("Preco")
                        .HasColumnType("integer")
                        .HasColumnName("preco");

                    b.Property<int>("empresa_id")
                        .HasColumnType("integer");

                    b.HasKey("ProdutoId");

                    b.HasIndex("empresa_id");

                    b.ToTable("produto");
                });

            modelBuilder.Entity("sme.src.Models.InstituicaoEnsino", b =>
                {
                    b.Property<int>("InstituicaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("instituicao_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("InstituicaoId"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.HasKey("InstituicaoId");

                    b.ToTable("instituicao");
                });

            modelBuilder.Entity("sme.src.Models.Professor", b =>
                {
                    b.Property<int>("ProfessorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("professor_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProfessorId"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cpf");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<int>("Moedas")
                        .HasColumnType("integer")
                        .HasColumnName("moedas");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("senha");

                    b.Property<int>("departamento_id")
                        .HasColumnType("integer");

                    b.HasKey("ProfessorId");

                    b.HasIndex("departamento_id");

                    b.ToTable("professor");
                });

            modelBuilder.Entity("sme.src.Models.Relations.ProfessorDepartamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("professor_departamento_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("departamento_id")
                        .HasColumnType("integer");

                    b.Property<int>("professor_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("departamento_id");

                    b.HasIndex("professor_id");

                    b.ToTable("professor_departamento");
                });

            modelBuilder.Entity("sme.src.Models.TransacaoEmpresa", b =>
                {
                    b.Property<int>("TransacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("transacao_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TransacaoId"));

                    b.Property<DateTime>("DataTransacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_transacao");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("motivo");

                    b.Property<int>("TipoTransacao")
                        .HasColumnType("integer")
                        .HasColumnName("tipo_transacao");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric")
                        .HasColumnName("valor");

                    b.Property<int>("aluno_id")
                        .HasColumnType("integer");

                    b.Property<int>("empresa_id")
                        .HasColumnType("integer");

                    b.Property<int>("produto_id")
                        .HasColumnType("integer");

                    b.HasKey("TransacaoId");

                    b.HasIndex("aluno_id");

                    b.HasIndex("empresa_id");

                    b.HasIndex("produto_id");

                    b.ToTable("transacao_aluno_empresa");
                });

            modelBuilder.Entity("sme.src.Models.TransacaoProfessor", b =>
                {
                    b.Property<int>("TransacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("transacao_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TransacaoId"));

                    b.Property<DateTime>("DataTransacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_transacao");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("motivo");

                    b.Property<int>("TipoTransacao")
                        .HasColumnType("integer")
                        .HasColumnName("tipo_transacao");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric")
                        .HasColumnName("valor");

                    b.Property<int>("aluno_id")
                        .HasColumnType("integer");

                    b.Property<int>("professor_id")
                        .HasColumnType("integer");

                    b.HasKey("TransacaoId");

                    b.HasIndex("aluno_id");

                    b.HasIndex("professor_id");

                    b.ToTable("transacao_professor_aluno");
                });

            modelBuilder.Entity("sme.src.Models.Aluno", b =>
                {
                    b.HasOne("sme.src.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("curso_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sme.src.Models.InstituicaoEnsino", "Instituicao")
                        .WithMany()
                        .HasForeignKey("instituicao_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Instituicao");
                });

            modelBuilder.Entity("sme.src.Models.Departamento", b =>
                {
                    b.HasOne("sme.src.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("curso_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sme.src.Models.InstituicaoEnsino", "Instituicao")
                        .WithMany()
                        .HasForeignKey("instituicao_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Instituicao");
                });

            modelBuilder.Entity("sme.src.Models.Empresa.Produto", b =>
                {
                    b.HasOne("sme.src.Models.Empresa.EmpresaParceira", "Empresa")
                        .WithMany()
                        .HasForeignKey("empresa_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("sme.src.Models.Professor", b =>
                {
                    b.HasOne("sme.src.Models.Departamento", "Departamento")
                        .WithMany()
                        .HasForeignKey("departamento_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("sme.src.Models.Relations.ProfessorDepartamento", b =>
                {
                    b.HasOne("sme.src.Models.Departamento", "Departamento")
                        .WithMany()
                        .HasForeignKey("departamento_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sme.src.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("professor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departamento");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("sme.src.Models.TransacaoEmpresa", b =>
                {
                    b.HasOne("sme.src.Models.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("aluno_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sme.src.Models.Empresa.EmpresaParceira", "EmpresaParceira")
                        .WithMany()
                        .HasForeignKey("empresa_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sme.src.Models.Empresa.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("produto_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aluno");

                    b.Navigation("EmpresaParceira");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("sme.src.Models.TransacaoProfessor", b =>
                {
                    b.HasOne("sme.src.Models.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("aluno_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sme.src.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("professor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aluno");

                    b.Navigation("Professor");
                });
#pragma warning restore 612, 618
        }
    }
}
