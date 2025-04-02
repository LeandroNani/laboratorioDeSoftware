﻿// <auto-generated />
using Backend.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250402015633_SeedAdmin")]
    partial class SeedAdmin
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("Quantidade")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AgenteId");

                    b.ToTable("Automoveis");
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

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@admin.com",
                            Nome = "Admin Seed",
                            SenhaHash = "$2a$11$z22flnDB6OGuDpfeM8t4f.TKM/Q5PXIaXD.im.RHplNfj2LBdEMzm"
                        });
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

                    b.Navigation("Agente");
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
