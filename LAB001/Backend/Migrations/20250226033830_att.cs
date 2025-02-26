using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class att : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoModelDisciplinaModel");

            migrationBuilder.AddColumn<int>(
                name: "AlunoModelNumeroDePessoa",
                table: "disciplina",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlunoModelNumeroDePessoa1",
                table: "disciplina",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "aluno_disciplina",
                columns: table => new
                {
                    AlunoId = table.Column<int>(type: "integer", nullable: false),
                    DisciplinaId = table.Column<int>(type: "integer", nullable: false),
                    DisciplinaId1 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluno_disciplina", x => new { x.AlunoId, x.DisciplinaId });
                    table.ForeignKey(
                        name: "FK_aluno_disciplina_aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "aluno",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aluno_disciplina_disciplina_DisciplinaId1",
                        column: x => x.DisciplinaId1,
                        principalTable: "disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_AlunoModelNumeroDePessoa",
                table: "disciplina",
                column: "AlunoModelNumeroDePessoa");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_AlunoModelNumeroDePessoa1",
                table: "disciplina",
                column: "AlunoModelNumeroDePessoa1");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_disciplina_DisciplinaId1",
                table: "aluno_disciplina",
                column: "DisciplinaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_disciplina_aluno_AlunoModelNumeroDePessoa",
                table: "disciplina",
                column: "AlunoModelNumeroDePessoa",
                principalTable: "aluno",
                principalColumn: "NumeroDePessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_disciplina_aluno_AlunoModelNumeroDePessoa1",
                table: "disciplina",
                column: "AlunoModelNumeroDePessoa1",
                principalTable: "aluno",
                principalColumn: "NumeroDePessoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_disciplina_aluno_AlunoModelNumeroDePessoa",
                table: "disciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_disciplina_aluno_AlunoModelNumeroDePessoa1",
                table: "disciplina");

            migrationBuilder.DropTable(
                name: "aluno_disciplina");

            migrationBuilder.DropIndex(
                name: "IX_disciplina_AlunoModelNumeroDePessoa",
                table: "disciplina");

            migrationBuilder.DropIndex(
                name: "IX_disciplina_AlunoModelNumeroDePessoa1",
                table: "disciplina");

            migrationBuilder.DropColumn(
                name: "AlunoModelNumeroDePessoa",
                table: "disciplina");

            migrationBuilder.DropColumn(
                name: "AlunoModelNumeroDePessoa1",
                table: "disciplina");

            migrationBuilder.CreateTable(
                name: "AlunoModelDisciplinaModel",
                columns: table => new
                {
                    AlunosNumeroDePessoa = table.Column<int>(type: "integer", nullable: false),
                    DisciplinasCursadasId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoModelDisciplinaModel", x => new { x.AlunosNumeroDePessoa, x.DisciplinasCursadasId });
                    table.ForeignKey(
                        name: "FK_AlunoModelDisciplinaModel_aluno_AlunosNumeroDePessoa",
                        column: x => x.AlunosNumeroDePessoa,
                        principalTable: "aluno",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoModelDisciplinaModel_disciplina_DisciplinasCursadasId",
                        column: x => x.DisciplinasCursadasId,
                        principalTable: "disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunoModelDisciplinaModel_DisciplinasCursadasId",
                table: "AlunoModelDisciplinaModel",
                column: "DisciplinasCursadasId");
        }
    }
}
