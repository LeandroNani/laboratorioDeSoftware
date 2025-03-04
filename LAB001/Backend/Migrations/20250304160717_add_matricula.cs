using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class add_matricula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_disciplina_aluno_AlunoModelNumeroDePessoa",
                table: "disciplina");

            migrationBuilder.DropColumn(
                name: "Matricula",
                table: "aluno");

            migrationBuilder.RenameColumn(
                name: "AlunoModelNumeroDePessoa",
                table: "disciplina",
                newName: "MatriculaModelNumeroDeMatricula");

            migrationBuilder.RenameIndex(
                name: "IX_disciplina_AlunoModelNumeroDePessoa",
                table: "disciplina",
                newName: "IX_disciplina_MatriculaModelNumeroDeMatricula");

            migrationBuilder.RenameColumn(
                name: "Mensalidade",
                table: "aluno",
                newName: "numero_matricula");

            migrationBuilder.CreateTable(
                name: "MatriculaModel",
                columns: table => new
                {
                    NumeroDeMatricula = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ativa = table.Column<bool>(type: "boolean", nullable: false),
                    Mensalidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatriculaModel", x => x.NumeroDeMatricula);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aluno_numero_matricula",
                table: "aluno",
                column: "numero_matricula");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_MatriculaModel_numero_matricula",
                table: "aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_disciplina_MatriculaModel_MatriculaModelNumeroDeMatricula",
                table: "disciplina");

            migrationBuilder.DropTable(
                name: "MatriculaModel");

            migrationBuilder.DropIndex(
                name: "IX_aluno_numero_matricula",
                table: "aluno");

            migrationBuilder.RenameColumn(
                name: "MatriculaModelNumeroDeMatricula",
                table: "disciplina",
                newName: "AlunoModelNumeroDePessoa");

            migrationBuilder.RenameIndex(
                name: "IX_disciplina_MatriculaModelNumeroDeMatricula",
                table: "disciplina",
                newName: "IX_disciplina_AlunoModelNumeroDePessoa");

            migrationBuilder.RenameColumn(
                name: "numero_matricula",
                table: "aluno",
                newName: "Mensalidade");

            migrationBuilder.AddColumn<string>(
                name: "Matricula",
                table: "aluno",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_disciplina_aluno_AlunoModelNumeroDePessoa",
                table: "disciplina",
                column: "AlunoModelNumeroDePessoa",
                principalTable: "aluno",
                principalColumn: "NumeroDePessoa");
        }
    }
}
