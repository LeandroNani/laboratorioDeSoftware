using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class change_matricula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_MatriculaModel_numero_matricula",
                table: "aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_disciplina_MatriculaModel_MatriculaModelNumeroDeMatricula",
                table: "disciplina");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatriculaModel",
                table: "MatriculaModel");

            migrationBuilder.RenameTable(
                name: "MatriculaModel",
                newName: "matricula");

            migrationBuilder.AddPrimaryKey(
                name: "PK_matricula",
                table: "matricula",
                column: "NumeroDeMatricula");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_matricula_numero_matricula",
                table: "aluno",
                column: "numero_matricula",
                principalTable: "matricula",
                principalColumn: "NumeroDeMatricula",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_disciplina_matricula_MatriculaModelNumeroDeMatricula",
                table: "disciplina",
                column: "MatriculaModelNumeroDeMatricula",
                principalTable: "matricula",
                principalColumn: "NumeroDeMatricula");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_matricula_numero_matricula",
                table: "aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_disciplina_matricula_MatriculaModelNumeroDeMatricula",
                table: "disciplina");

            migrationBuilder.DropPrimaryKey(
                name: "PK_matricula",
                table: "matricula");

            migrationBuilder.RenameTable(
                name: "matricula",
                newName: "MatriculaModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatriculaModel",
                table: "MatriculaModel",
                column: "NumeroDeMatricula");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_MatriculaModel_numero_matricula",
                table: "aluno",
                column: "numero_matricula",
                principalTable: "MatriculaModel",
                principalColumn: "NumeroDeMatricula",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_disciplina_MatriculaModel_MatriculaModelNumeroDeMatricula",
                table: "disciplina",
                column: "MatriculaModelNumeroDeMatricula",
                principalTable: "MatriculaModel",
                principalColumn: "NumeroDeMatricula");
        }
    }
}
