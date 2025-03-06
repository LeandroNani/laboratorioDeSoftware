using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class fff : Migration
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
                name: "matricula",
                columns: table => new
                {
                    NumeroDeMatricula = table.Column<string>(type: "text", nullable: false),
                    Ativa = table.Column<bool>(type: "boolean", nullable: false),
                    Mensalidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matricula", x => x.NumeroDeMatricula);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.NumeroDePessoa);
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
                name: "pessoa_admin",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoa_admin", x => x.NumeroDePessoa);
                    table.ForeignKey(
                        name: "FK_pessoa_admin_Pessoas_NumeroDePessoa",
                        column: x => x.NumeroDePessoa,
                        principalTable: "Pessoas",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "professor",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<string>(type: "text", nullable: false),
                    NivelEscolar = table.Column<string>(type: "text", nullable: false),
                    CurriculoModelId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professor", x => x.NumeroDePessoa);
                    table.ForeignKey(
                        name: "FK_professor_Pessoas_NumeroDePessoa",
                        column: x => x.NumeroDePessoa,
                        principalTable: "Pessoas",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_professor_curriculo_CurriculoModelId",
                        column: x => x.CurriculoModelId,
                        principalTable: "curriculo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "aluno",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<string>(type: "text", nullable: false),
                    MatriculaId = table.Column<string>(type: "text", nullable: false),
                    CursoId = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    aluno_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aluno", x => x.NumeroDePessoa);
                    table.ForeignKey(
                        name: "FK_aluno_Pessoas_NumeroDePessoa",
                        column: x => x.NumeroDePessoa,
                        principalTable: "Pessoas",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aluno_curriculo_aluno_id",
                        column: x => x.aluno_id,
                        principalTable: "curriculo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_aluno_curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "curso",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_aluno_matricula_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "matricula",
                        principalColumn: "NumeroDeMatricula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "disciplina",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ProfessorId = table.Column<string>(type: "text", nullable: false),
                    Preco = table.Column<int>(type: "integer", nullable: false),
                    Periodo = table.Column<string>(type: "text", nullable: false),
                    DisciplinasNecessarias = table.Column<string>(type: "text", nullable: true),
                    Campus = table.Column<string>(type: "text", nullable: false),
                    Optativa = table.Column<bool>(type: "boolean", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    QuantAlunos = table.Column<int>(type: "integer", nullable: false),
                    AlunoModelNumeroDePessoa = table.Column<string>(type: "text", nullable: true),
                    CursoModelId = table.Column<string>(type: "text", nullable: true),
                    MatriculaModelNumeroDeMatricula = table.Column<string>(type: "text", nullable: true),
                    disciplina_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disciplina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_disciplina_aluno_AlunoModelNumeroDePessoa",
                        column: x => x.AlunoModelNumeroDePessoa,
                        principalTable: "aluno",
                        principalColumn: "NumeroDePessoa");
                    table.ForeignKey(
                        name: "FK_disciplina_curriculo_disciplina_id",
                        column: x => x.disciplina_id,
                        principalTable: "curriculo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_disciplina_curso_CursoModelId",
                        column: x => x.CursoModelId,
                        principalTable: "curso",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_disciplina_matricula_MatriculaModelNumeroDeMatricula",
                        column: x => x.MatriculaModelNumeroDeMatricula,
                        principalTable: "matricula",
                        principalColumn: "NumeroDeMatricula");
                    table.ForeignKey(
                        name: "FK_disciplina_professor_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "professor",
                        principalColumn: "NumeroDePessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aluno_aluno_id",
                table: "aluno",
                column: "aluno_id");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_CursoId",
                table: "aluno",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_MatriculaId",
                table: "aluno",
                column: "MatriculaId");

            migrationBuilder.CreateIndex(
                name: "IX_curso_CurriculoModelId",
                table: "curso",
                column: "CurriculoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_AlunoModelNumeroDePessoa",
                table: "disciplina",
                column: "AlunoModelNumeroDePessoa");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_CursoModelId",
                table: "disciplina",
                column: "CursoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_disciplina_id",
                table: "disciplina",
                column: "disciplina_id");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_MatriculaModelNumeroDeMatricula",
                table: "disciplina",
                column: "MatriculaModelNumeroDeMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_ProfessorId",
                table: "disciplina",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_professor_CurriculoModelId",
                table: "professor",
                column: "CurriculoModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "disciplina");

            migrationBuilder.DropTable(
                name: "pessoa_admin");

            migrationBuilder.DropTable(
                name: "aluno");

            migrationBuilder.DropTable(
                name: "professor");

            migrationBuilder.DropTable(
                name: "curso");

            migrationBuilder.DropTable(
                name: "matricula");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "curriculo");
        }
    }
}
