using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class fix_curri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_curso_CursoModelId",
                table: "aluno");

            migrationBuilder.DropIndex(
                name: "IX_aluno_CursoModelId",
                table: "aluno");

            migrationBuilder.DropColumn(
                name: "CursoModelId",
                table: "aluno");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_CursoId",
                table: "aluno",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_curso_CursoId",
                table: "aluno",
                column: "CursoId",
                principalTable: "curso",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_curso_CursoId",
                table: "aluno");

            migrationBuilder.DropIndex(
                name: "IX_aluno_CursoId",
                table: "aluno");

            migrationBuilder.AddColumn<string>(
                name: "CursoModelId",
                table: "aluno",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_aluno_CursoModelId",
                table: "aluno",
                column: "CursoModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_curso_CursoModelId",
                table: "aluno",
                column: "CursoModelId",
                principalTable: "curso",
                principalColumn: "Id");
        }
    }
}
