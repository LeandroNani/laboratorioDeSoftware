using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                name: "pessoa",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoa", x => x.NumeroDePessoa);
                });

            migrationBuilder.CreateTable(
                name: "curso",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    NumeroDeCreditos = table.Column<int>(type: "integer", nullable: false),
                    CurriculoModelId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_curso_curriculo_CurriculoModelId",
                        column: x => x.CurriculoModelId,
                        principalTable: "curriculo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "professor",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<int>(type: "integer", nullable: false),
                    NivelEscolar = table.Column<string>(type: "text", nullable: false),
                    CurriculoModelId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professor", x => x.NumeroDePessoa);
                    table.ForeignKey(
                        name: "FK_professor_curriculo_CurriculoModelId",
                        column: x => x.CurriculoModelId,
                        principalTable: "curriculo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_professor_pessoa_NumeroDePessoa",
                        column: x => x.NumeroDePessoa,
                        principalTable: "pessoa",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aluno",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<int>(type: "integer", nullable: false),
                    CursoId = table.Column<string>(type: "text", nullable: true),
                    Matricula = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Mensalidade = table.Column<int>(type: "integer", nullable: false),
                    CurriculoModelId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluno", x => x.NumeroDePessoa);
                    table.ForeignKey(
                        name: "FK_aluno_curriculo_CurriculoModelId",
                        column: x => x.CurriculoModelId,
                        principalTable: "curriculo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_aluno_curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "curso",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_aluno_pessoa_NumeroDePessoa",
                        column: x => x.NumeroDePessoa,
                        principalTable: "pessoa",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "disciplina",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ProfessorNumeroDePessoa = table.Column<int>(type: "integer", nullable: false),
                    Precos = table.Column<int>(type: "integer", nullable: false),
                    Periodo = table.Column<string>(type: "text", nullable: false),
                    Campus = table.Column<string>(type: "text", nullable: false),
                    CurriculoModelId = table.Column<string>(type: "text", nullable: true),
                    CursoModelId = table.Column<string>(type: "text", nullable: true),
                    DisciplinaModelId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disciplina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_disciplina_curriculo_CurriculoModelId",
                        column: x => x.CurriculoModelId,
                        principalTable: "curriculo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_disciplina_curso_CursoModelId",
                        column: x => x.CursoModelId,
                        principalTable: "curso",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_disciplina_disciplina_DisciplinaModelId",
                        column: x => x.DisciplinaModelId,
                        principalTable: "disciplina",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_disciplina_professor_ProfessorNumeroDePessoa",
                        column: x => x.ProfessorNumeroDePessoa,
                        principalTable: "professor",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunoModelDisciplinaModel",
                columns: table => new
                {
                    AlunosNumeroDePessoa = table.Column<int>(type: "integer", nullable: false),
                    DisciplinasFeitasId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoModelDisciplinaModel", x => new { x.AlunosNumeroDePessoa, x.DisciplinasFeitasId });
                    table.ForeignKey(
                        name: "FK_AlunoModelDisciplinaModel_aluno_AlunosNumeroDePessoa",
                        column: x => x.AlunosNumeroDePessoa,
                        principalTable: "aluno",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoModelDisciplinaModel_disciplina_DisciplinasFeitasId",
                        column: x => x.DisciplinasFeitasId,
                        principalTable: "disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aluno_CurriculoModelId",
                table: "aluno",
                column: "CurriculoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_CursoId",
                table: "aluno",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoModelDisciplinaModel_DisciplinasFeitasId",
                table: "AlunoModelDisciplinaModel",
                column: "DisciplinasFeitasId");

            migrationBuilder.CreateIndex(
                name: "IX_curso_CurriculoModelId",
                table: "curso",
                column: "CurriculoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_CurriculoModelId",
                table: "disciplina",
                column: "CurriculoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_CursoModelId",
                table: "disciplina",
                column: "CursoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_DisciplinaModelId",
                table: "disciplina",
                column: "DisciplinaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_ProfessorNumeroDePessoa",
                table: "disciplina",
                column: "ProfessorNumeroDePessoa");

            migrationBuilder.CreateIndex(
                name: "IX_professor_CurriculoModelId",
                table: "professor",
                column: "CurriculoModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoModelDisciplinaModel");

            migrationBuilder.DropTable(
                name: "aluno");

            migrationBuilder.DropTable(
                name: "disciplina");

            migrationBuilder.DropTable(
                name: "curso");

            migrationBuilder.DropTable(
                name: "professor");

            migrationBuilder.DropTable(
                name: "curriculo");

            migrationBuilder.DropTable(
                name: "pessoa");
        }
    }
}
