using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class fix_curriculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_professor_curriculo_USING professor_id::integer",
                table: "professor");

            migrationBuilder.RenameColumn(
                name: "USING professor_id::integer",
                table: "professor",
                newName: "CurriculoModelId");

            migrationBuilder.RenameIndex(
                name: "IX_professor_USING professor_id::integer",
                table: "professor",
                newName: "IX_professor_CurriculoModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_professor_curriculo_CurriculoModelId",
                table: "professor",
                column: "CurriculoModelId",
                principalTable: "curriculo",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_professor_curriculo_CurriculoModelId",
                table: "professor");

            migrationBuilder.RenameColumn(
                name: "CurriculoModelId",
                table: "professor",
                newName: "USING professor_id::integer");

            migrationBuilder.RenameIndex(
                name: "IX_professor_CurriculoModelId",
                table: "professor",
                newName: "IX_professor_USING professor_id::integer");

            migrationBuilder.AddForeignKey(
                name: "FK_professor_curriculo_USING professor_id::integer",
                table: "professor",
                column: "USING professor_id::integer",
                principalTable: "curriculo",
                principalColumn: "Id");
        }
    }
}
