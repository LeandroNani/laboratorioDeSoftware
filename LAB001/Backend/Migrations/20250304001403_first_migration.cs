using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class first_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "curriculo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Semestre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curriculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.NumeroDePessoa);
                });

            migrationBuilder.CreateTable(
                name: "curso",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    NumeroDeCreditos = table.Column<int>(type: "integer", nullable: false),
                    curso_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_curso_curriculo_curso_id",
                        column: x => x.curso_id,
                        principalTable: "curriculo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "pessoa_admin",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoa_admin", x => x.NumeroDePessoa);
                    table.ForeignKey(
                        name: "FK_pessoa_admin_Pessoas_NumeroDePessoa",
                        column: x => x.NumeroDePessoa,
                        principalTable: "Pessoas",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "professor",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<int>(type: "integer", nullable: false),
                    NivelEscolar = table.Column<string>(type: "text", nullable: false),
                    professor_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professor", x => x.NumeroDePessoa);
                    table.ForeignKey(
                        name: "FK_professor_Pessoas_NumeroDePessoa",
                        column: x => x.NumeroDePessoa,
                        principalTable: "Pessoas",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_professor_curriculo_professor_id",
                        column: x => x.professor_id,
                        principalTable: "curriculo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "aluno",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<int>(type: "integer", nullable: false),
                    curso_id = table.Column<string>(type: "text", nullable: true),
                    Matricula = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Mensalidade = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    aluno_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluno", x => x.NumeroDePessoa);
                    table.ForeignKey(
                        name: "FK_aluno_Pessoas_NumeroDePessoa",
                        column: x => x.NumeroDePessoa,
                        principalTable: "Pessoas",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aluno_curriculo_aluno_id",
                        column: x => x.aluno_id,
                        principalTable: "curriculo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_aluno_curso_curso_id",
                        column: x => x.curso_id,
                        principalTable: "curso",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "disciplina",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    professor_id = table.Column<int>(type: "integer", nullable: false),
                    Preco = table.Column<int>(type: "integer", nullable: false),
                    Periodo = table.Column<string>(type: "text", nullable: false),
                    Campus = table.Column<string>(type: "text", nullable: false),
                    Optativa = table.Column<bool>(type: "boolean", nullable: false),
                    QuantAlunos = table.Column<int>(type: "integer", nullable: false),
                    AlunoModelNumeroDePessoa = table.Column<int>(type: "integer", nullable: true),
                    disciplina_cursada_id = table.Column<int>(type: "integer", nullable: true),
                    disciplina_id = table.Column<string>(type: "text", nullable: true),
                    disciplina_necessaria_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disciplina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_disciplina_aluno_AlunoModelNumeroDePessoa",
                        column: x => x.AlunoModelNumeroDePessoa,
                        principalTable: "aluno",
                        principalColumn: "NumeroDePessoa");
                    table.ForeignKey(
                        name: "FK_disciplina_aluno_disciplina_cursada_id",
                        column: x => x.disciplina_cursada_id,
                        principalTable: "aluno",
                        principalColumn: "NumeroDePessoa");
                    table.ForeignKey(
                        name: "FK_disciplina_curriculo_disciplina_id",
                        column: x => x.disciplina_id,
                        principalTable: "curriculo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_disciplina_curso_disciplina_id",
                        column: x => x.disciplina_id,
                        principalTable: "curso",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_disciplina_disciplina_disciplina_necessaria_id",
                        column: x => x.disciplina_necessaria_id,
                        principalTable: "disciplina",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_disciplina_professor_professor_id",
                        column: x => x.professor_id,
                        principalTable: "professor",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aluno_disciplina",
                columns: table => new
                {
                    AlunoId = table.Column<int>(type: "integer", nullable: false),
                    DisciplinaId = table.Column<int>(type: "integer", nullable: false),
                    aluno_id = table.Column<int>(type: "integer", nullable: false),
                    disciplina_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluno_disciplina", x => new { x.AlunoId, x.DisciplinaId });
                    table.ForeignKey(
                        name: "FK_aluno_disciplina_aluno_aluno_id",
                        column: x => x.aluno_id,
                        principalTable: "aluno",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aluno_disciplina_disciplina_disciplina_id",
                        column: x => x.disciplina_id,
                        principalTable: "disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aluno_aluno_id",
                table: "aluno",
                column: "aluno_id");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_curso_id",
                table: "aluno",
                column: "curso_id");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_disciplina_aluno_id",
                table: "aluno_disciplina",
                column: "aluno_id");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_disciplina_disciplina_id",
                table: "aluno_disciplina",
                column: "disciplina_id");

            migrationBuilder.CreateIndex(
                name: "IX_curso_curso_id",
                table: "curso",
                column: "curso_id");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_AlunoModelNumeroDePessoa",
                table: "disciplina",
                column: "AlunoModelNumeroDePessoa");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_disciplina_cursada_id",
                table: "disciplina",
                column: "disciplina_cursada_id");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_disciplina_id",
                table: "disciplina",
                column: "disciplina_id");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_disciplina_necessaria_id",
                table: "disciplina",
                column: "disciplina_necessaria_id");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_professor_id",
                table: "disciplina",
                column: "professor_id");

            migrationBuilder.CreateIndex(
                name: "IX_professor_professor_id",
                table: "professor",
                column: "professor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aluno_disciplina");

            migrationBuilder.DropTable(
                name: "pessoa_admin");

            migrationBuilder.DropTable(
                name: "disciplina");

            migrationBuilder.DropTable(
                name: "aluno");

            migrationBuilder.DropTable(
                name: "professor");

            migrationBuilder.DropTable(
                name: "curso");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "curriculo");
        }
    }
}
