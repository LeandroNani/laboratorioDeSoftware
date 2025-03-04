using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class change_curriculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_curso_curriculo_curso_id",
                table: "curso");

            migrationBuilder.DropIndex(
                name: "IX_curso_curso_id",
                table: "curso");

            migrationBuilder.DropColumn(
                name: "curso_id",
                table: "curso");

            migrationBuilder.AddColumn<string>(
                name: "curso_id",
                table: "curriculo",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_curso_id",
                table: "curriculo",
                column: "curso_id");

            migrationBuilder.AddForeignKey(
                name: "FK_curriculo_curso_curso_id",
                table: "curriculo",
                column: "curso_id",
                principalTable: "curso",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_curriculo_curso_curso_id",
                table: "curriculo");

            migrationBuilder.DropIndex(
                name: "IX_curriculo_curso_id",
                table: "curriculo");

            migrationBuilder.DropColumn(
                name: "curso_id",
                table: "curriculo");

            migrationBuilder.AddColumn<string>(
                name: "curso_id",
                table: "curso",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_curso_curso_id",
                table: "curso",
                column: "curso_id");

            migrationBuilder.AddForeignKey(
                name: "FK_curso_curriculo_curso_id",
                table: "curso",
                column: "curso_id",
                principalTable: "curriculo",
                principalColumn: "Id");
        }
    }
}
