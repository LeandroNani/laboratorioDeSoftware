using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class changenames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlunoModelDisciplinaModel_disciplina_DisciplinasFeitasId",
                table: "AlunoModelDisciplinaModel");

            migrationBuilder.RenameColumn(
                name: "DisciplinasFeitasId",
                table: "AlunoModelDisciplinaModel",
                newName: "DisciplinasCursadasId");

            migrationBuilder.RenameIndex(
                name: "IX_AlunoModelDisciplinaModel_DisciplinasFeitasId",
                table: "AlunoModelDisciplinaModel",
                newName: "IX_AlunoModelDisciplinaModel_DisciplinasCursadasId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoModelDisciplinaModel_disciplina_DisciplinasCursadasId",
                table: "AlunoModelDisciplinaModel",
                column: "DisciplinasCursadasId",
                principalTable: "disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlunoModelDisciplinaModel_disciplina_DisciplinasCursadasId",
                table: "AlunoModelDisciplinaModel");

            migrationBuilder.RenameColumn(
                name: "DisciplinasCursadasId",
                table: "AlunoModelDisciplinaModel",
                newName: "DisciplinasFeitasId");

            migrationBuilder.RenameIndex(
                name: "IX_AlunoModelDisciplinaModel_DisciplinasCursadasId",
                table: "AlunoModelDisciplinaModel",
                newName: "IX_AlunoModelDisciplinaModel_DisciplinasFeitasId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoModelDisciplinaModel_disciplina_DisciplinasFeitasId",
                table: "AlunoModelDisciplinaModel",
                column: "DisciplinasFeitasId",
                principalTable: "disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
