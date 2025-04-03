﻿// <auto-generated />
using System;
using Backend.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Backend.API.Model.Automovel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AgenteId")
                        .HasColumnType("integer");

                    b.Property<int>("Ano")
                        .HasColumnType("integer");

                    b.Property<int?>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<string>("FotoUrl")
                        .HasColumnType("text");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Matricula")
                        .HasColumnType("integer");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AgenteId");

                    b.HasIndex("ClienteId");

                    b.ToTable("Automoveis");
                });

            modelBuilder.Entity("Backend.API.Model.Contrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AgenteId")
                        .HasColumnType("integer");

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PedidoId")
                        .HasColumnType("integer");

                    b.Property<string>("TipoContrato")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AgenteId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("PedidoId");

                    b.ToTable("Contratos");
                });

            modelBuilder.Entity("Backend.API.Model.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AgenteDesignadoId")
                        .HasColumnType("integer");

                    b.Property<int>("AutomovelId")
                        .HasColumnType("integer");

                    b.Property<int>("ContratanteId")
                        .HasColumnType("integer");

                    b.Property<int>("Duracao")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("False");

                    b.Property<string>("TipoContrato")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AgenteDesignadoId");

                    b.HasIndex("AutomovelId");

                    b.HasIndex("ContratanteId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Backend.API.Model.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Endereco")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("SenhaHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");

                    b.HasDiscriminator().HasValue("Usuario");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Backend.API.Model.Admin", b =>
                {
                    b.HasBaseType("Backend.API.Model.Usuario");

                    b.HasDiscriminator().HasValue("ADMIN");
                });

            modelBuilder.Entity("Backend.API.Model.Agente", b =>
                {
                    b.HasBaseType("Backend.API.Model.Usuario");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("QuantidadeCarros")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("AGENTE");
                });

            modelBuilder.Entity("Backend.API.Model.Cliente", b =>
                {
                    b.HasBaseType("Backend.API.Model.Usuario");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EntidadeEmpregadora")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Profissao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RG")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rendimentos")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasDiscriminator().HasValue("CLIENTE");
                });

            modelBuilder.Entity("Backend.API.Model.Automovel", b =>
                {
                    b.HasOne("Backend.API.Model.Agente", "Agente")
                        .WithMany("Automoveis")
                        .HasForeignKey("AgenteId");

                    b.HasOne("Backend.API.Model.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.Navigation("Agente");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("Backend.API.Model.Contrato", b =>
                {
                    b.HasOne("Backend.API.Model.Agente", "Agente")
                        .WithMany()
                        .HasForeignKey("AgenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.API.Model.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.API.Model.Pedido", "Pedido")
                        .WithMany()
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agente");

                    b.Navigation("Cliente");

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("Backend.API.Model.Pedido", b =>
                {
                    b.HasOne("Backend.API.Model.Agente", "AgenteDesignado")
                        .WithMany("PedidosDesignados")
                        .HasForeignKey("AgenteDesignadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.API.Model.Automovel", "Automovel")
                        .WithMany("Pedidos")
                        .HasForeignKey("AutomovelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.API.Model.Cliente", "Contratante")
                        .WithMany("Pedidos")
                        .HasForeignKey("ContratanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AgenteDesignado");

                    b.Navigation("Automovel");

                    b.Navigation("Contratante");
                });

            modelBuilder.Entity("Backend.API.Model.Automovel", b =>
                {
                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("Backend.API.Model.Agente", b =>
                {
                    b.Navigation("Automoveis");

                    b.Navigation("PedidosDesignados");
                });

            modelBuilder.Entity("Backend.API.Model.Cliente", b =>
                {
                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
