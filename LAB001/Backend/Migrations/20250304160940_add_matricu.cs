using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class add_matricu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aluno_disciplina");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_aluno_disciplina_aluno_id",
                table: "aluno_disciplina",
                column: "aluno_id");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_disciplina_disciplina_id",
                table: "aluno_disciplina",
                column: "disciplina_id");
        }
    }
}
