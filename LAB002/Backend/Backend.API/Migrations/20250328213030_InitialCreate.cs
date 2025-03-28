using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Endereco = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    SenhaHash = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    CNPJ = table.Column<string>(type: "text", nullable: true),
                    QuantidadeCarros = table.Column<int>(type: "integer", nullable: true),
                    RG = table.Column<string>(type: "text", nullable: true),
                    CPF = table.Column<string>(type: "text", nullable: true),
                    Profissao = table.Column<string>(type: "text", nullable: true),
                    EntidadeEmpregadora = table.Column<string>(type: "text", nullable: true),
                    Rendimentos = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Automoveis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Matricula = table.Column<int>(type: "integer", nullable: false),
                    Ano = table.Column<int>(type: "integer", nullable: false),
                    Marca = table.Column<string>(type: "text", nullable: false),
                    Modelo = table.Column<string>(type: "text", nullable: false),
                    Placa = table.Column<string>(type: "text", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    AgenteId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automoveis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Automoveis_Usuarios_AgenteId",
                        column: x => x.AgenteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ContratanteId = table.Column<int>(type: "integer", nullable: false),
                    AgenteDesignadoId = table.Column<int>(type: "integer", nullable: false),
                    AutomovelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Automoveis_AutomovelId",
                        column: x => x.AutomovelId,
                        principalTable: "Automoveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_AgenteDesignadoId",
                        column: x => x.AgenteDesignadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_ContratanteId",
                        column: x => x.ContratanteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automoveis_AgenteId",
                table: "Automoveis",
                column: "AgenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_AgenteDesignadoId",
                table: "Pedidos",
                column: "AgenteDesignadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_AutomovelId",
                table: "Pedidos",
                column: "AutomovelId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ContratanteId",
                table: "Pedidos",
                column: "ContratanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Automoveis");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
