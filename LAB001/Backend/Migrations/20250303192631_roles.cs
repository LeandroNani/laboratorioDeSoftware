using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_pessoa_NumeroDePessoa",
                table: "aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_professor_pessoa_NumeroDePessoa",
                table: "professor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pessoa",
                table: "pessoa");

            migrationBuilder.RenameTable(
                name: "pessoa",
                newName: "Pessoas");

            migrationBuilder.AddColumn<bool>(
                name: "Optativa",
                table: "disciplina",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pessoas",
                table: "Pessoas",
                column: "NumeroDePessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_Pessoas_NumeroDePessoa",
                table: "aluno",
                column: "NumeroDePessoa",
                principalTable: "Pessoas",
                principalColumn: "NumeroDePessoa",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_professor_Pessoas_NumeroDePessoa",
                table: "professor",
                column: "NumeroDePessoa",
                principalTable: "Pessoas",
                principalColumn: "NumeroDePessoa",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_Pessoas_NumeroDePessoa",
                table: "aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_professor_Pessoas_NumeroDePessoa",
                table: "professor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pessoas",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "Optativa",
                table: "disciplina");

            migrationBuilder.RenameTable(
                name: "Pessoas",
                newName: "pessoa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pessoa",
                table: "pessoa",
                column: "NumeroDePessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_pessoa_NumeroDePessoa",
                table: "aluno",
                column: "NumeroDePessoa",
                principalTable: "pessoa",
                principalColumn: "NumeroDePessoa",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_professor_pessoa_NumeroDePessoa",
                table: "professor",
                column: "NumeroDePessoa",
                principalTable: "pessoa",
                principalColumn: "NumeroDePessoa",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
