using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace sme.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "curso",
                columns: table => new
                {
                    curso_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curso", x => x.curso_id);
                });

            migrationBuilder.CreateTable(
                name: "empresa_parceira",
                columns: table => new
                {
                    empresa_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresa_parceira", x => x.empresa_id);
                });

            migrationBuilder.CreateTable(
                name: "instituicao",
                columns: table => new
                {
                    instituicao_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instituicao", x => x.instituicao_id);
                });

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    produto_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    foto = table.Column<byte[]>(type: "bytea", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    preco = table.Column<int>(type: "integer", nullable: false),
                    empresa_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto", x => x.produto_id);
                    table.ForeignKey(
                        name: "FK_produto_empresa_parceira_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresa_parceira",
                        principalColumn: "empresa_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aluno",
                columns: table => new
                {
                    aluno_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rg = table.Column<string>(type: "text", nullable: false),
                    cpf = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    instituicao_id = table.Column<int>(type: "integer", nullable: false),
                    curso_id = table.Column<int>(type: "integer", nullable: false),
                    moedas = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluno", x => x.aluno_id);
                    table.ForeignKey(
                        name: "FK_aluno_curso_curso_id",
                        column: x => x.curso_id,
                        principalTable: "curso",
                        principalColumn: "curso_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aluno_instituicao_instituicao_id",
                        column: x => x.instituicao_id,
                        principalTable: "instituicao",
                        principalColumn: "instituicao_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "departamento",
                columns: table => new
                {
                    departamento_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    instituicao_id = table.Column<int>(type: "integer", nullable: false),
                    curso_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departamento", x => x.departamento_id);
                    table.ForeignKey(
                        name: "FK_departamento_curso_curso_id",
                        column: x => x.curso_id,
                        principalTable: "curso",
                        principalColumn: "curso_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_departamento_instituicao_instituicao_id",
                        column: x => x.instituicao_id,
                        principalTable: "instituicao",
                        principalColumn: "instituicao_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transacao_aluno_empresa",
                columns: table => new
                {
                    transacao_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    empresa_id = table.Column<int>(type: "integer", nullable: false),
                    produto_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_transacao = table.Column<int>(type: "integer", nullable: false),
                    aluno_id = table.Column<int>(type: "integer", nullable: false),
                    motivo = table.Column<string>(type: "text", nullable: false),
                    valor = table.Column<decimal>(type: "numeric", nullable: false),
                    data_transacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transacao_aluno_empresa", x => x.transacao_id);
                    table.ForeignKey(
                        name: "FK_transacao_aluno_empresa_aluno_aluno_id",
                        column: x => x.aluno_id,
                        principalTable: "aluno",
                        principalColumn: "aluno_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transacao_aluno_empresa_empresa_parceira_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresa_parceira",
                        principalColumn: "empresa_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transacao_aluno_empresa_produto_produto_id",
                        column: x => x.produto_id,
                        principalTable: "produto",
                        principalColumn: "produto_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "professor",
                columns: table => new
                {
                    professor_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    cpf = table.Column<string>(type: "text", nullable: false),
                    departamento_id = table.Column<int>(type: "integer", nullable: false),
                    moedas = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professor", x => x.professor_id);
                    table.ForeignKey(
                        name: "FK_professor_departamento_departamento_id",
                        column: x => x.departamento_id,
                        principalTable: "departamento",
                        principalColumn: "departamento_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "professor_departamento",
                columns: table => new
                {
                    professor_departamento_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    professor_id = table.Column<int>(type: "integer", nullable: false),
                    departamento_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professor_departamento", x => x.professor_departamento_id);
                    table.ForeignKey(
                        name: "FK_professor_departamento_departamento_departamento_id",
                        column: x => x.departamento_id,
                        principalTable: "departamento",
                        principalColumn: "departamento_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_professor_departamento_professor_professor_id",
                        column: x => x.professor_id,
                        principalTable: "professor",
                        principalColumn: "professor_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transacao_professor_aluno",
                columns: table => new
                {
                    transacao_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    professor_id = table.Column<int>(type: "integer", nullable: false),
                    tipo_transacao = table.Column<int>(type: "integer", nullable: false),
                    aluno_id = table.Column<int>(type: "integer", nullable: false),
                    motivo = table.Column<string>(type: "text", nullable: false),
                    valor = table.Column<decimal>(type: "numeric", nullable: false),
                    data_transacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transacao_professor_aluno", x => x.transacao_id);
                    table.ForeignKey(
                        name: "FK_transacao_professor_aluno_aluno_aluno_id",
                        column: x => x.aluno_id,
                        principalTable: "aluno",
                        principalColumn: "aluno_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transacao_professor_aluno_professor_professor_id",
                        column: x => x.professor_id,
                        principalTable: "professor",
                        principalColumn: "professor_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aluno_curso_id",
                table: "aluno",
                column: "curso_id");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_instituicao_id",
                table: "aluno",
                column: "instituicao_id");

            migrationBuilder.CreateIndex(
                name: "IX_departamento_curso_id",
                table: "departamento",
                column: "curso_id");

            migrationBuilder.CreateIndex(
                name: "IX_departamento_instituicao_id",
                table: "departamento",
                column: "instituicao_id");

            migrationBuilder.CreateIndex(
                name: "IX_produto_empresa_id",
                table: "produto",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_professor_departamento_id",
                table: "professor",
                column: "departamento_id");

            migrationBuilder.CreateIndex(
                name: "IX_professor_departamento_departamento_id",
                table: "professor_departamento",
                column: "departamento_id");

            migrationBuilder.CreateIndex(
                name: "IX_professor_departamento_professor_id",
                table: "professor_departamento",
                column: "professor_id");

            migrationBuilder.CreateIndex(
                name: "IX_transacao_aluno_empresa_aluno_id",
                table: "transacao_aluno_empresa",
                column: "aluno_id");

            migrationBuilder.CreateIndex(
                name: "IX_transacao_aluno_empresa_empresa_id",
                table: "transacao_aluno_empresa",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_transacao_aluno_empresa_produto_id",
                table: "transacao_aluno_empresa",
                column: "produto_id");

            migrationBuilder.CreateIndex(
                name: "IX_transacao_professor_aluno_aluno_id",
                table: "transacao_professor_aluno",
                column: "aluno_id");

            migrationBuilder.CreateIndex(
                name: "IX_transacao_professor_aluno_professor_id",
                table: "transacao_professor_aluno",
                column: "professor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "professor_departamento");

            migrationBuilder.DropTable(
                name: "transacao_aluno_empresa");

            migrationBuilder.DropTable(
                name: "transacao_professor_aluno");

            migrationBuilder.DropTable(
                name: "produto");

            migrationBuilder.DropTable(
                name: "aluno");

            migrationBuilder.DropTable(
                name: "professor");

            migrationBuilder.DropTable(
                name: "empresa_parceira");

            migrationBuilder.DropTable(
                name: "departamento");

            migrationBuilder.DropTable(
                name: "curso");

            migrationBuilder.DropTable(
                name: "instituicao");
        }
    }
}
