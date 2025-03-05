using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "curso",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    NumeroDeCreditos = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curso", x => x.Id);
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
                name: "curriculo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    curso_id = table.Column<string>(type: "text", nullable: true),
                    Semestre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curriculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_curriculo_curso_curso_id",
                        column: x => x.curso_id,
                        principalTable: "curso",
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
                name: "aluno",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<string>(type: "text", nullable: false),
                    CursoId = table.Column<string>(type: "text", nullable: true),
                    MatriculaId = table.Column<string>(type: "text", nullable: false),
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
                name: "professor",
                columns: table => new
                {
                    NumeroDePessoa = table.Column<string>(type: "text", nullable: false),
                    NivelEscolar = table.Column<string>(type: "text", nullable: false),
                    USINGprofessor_idinteger = table.Column<string>(name: "USING professor_id::integer", type: "text", nullable: true)
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
                        name: "FK_professor_curriculo_USING professor_id::integer",
                        column: x => x.USINGprofessor_idinteger,
                        principalTable: "curriculo",
                        principalColumn: "Id");
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
                    Campus = table.Column<string>(type: "text", nullable: false),
                    Optativa = table.Column<bool>(type: "boolean", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    QuantAlunos = table.Column<int>(type: "integer", nullable: false),
                    AlunoModelNumeroDePessoa = table.Column<string>(type: "text", nullable: true),
                    DisciplinaModelId = table.Column<string>(type: "text", nullable: true),
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
                        name: "FK_disciplina_curso_disciplina_id",
                        column: x => x.disciplina_id,
                        principalTable: "curso",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_disciplina_disciplina_DisciplinaModelId",
                        column: x => x.DisciplinaModelId,
                        principalTable: "disciplina",
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

            migrationBuilder.CreateTable(
                name: "aluno_disciplina",
                columns: table => new
                {
                    AlunoId = table.Column<string>(type: "text", nullable: false),
                    DisciplinaId = table.Column<string>(type: "text", nullable: false),
                    aluno_id = table.Column<string>(type: "text", nullable: false),
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
                name: "IX_aluno_disciplina_aluno_id",
                table: "aluno_disciplina",
                column: "aluno_id");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_disciplina_disciplina_id",
                table: "aluno_disciplina",
                column: "disciplina_id");

            migrationBuilder.CreateIndex(
                name: "IX_curriculo_curso_id",
                table: "curriculo",
                column: "curso_id");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_AlunoModelNumeroDePessoa",
                table: "disciplina",
                column: "AlunoModelNumeroDePessoa");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_disciplina_id",
                table: "disciplina",
                column: "disciplina_id");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_DisciplinaModelId",
                table: "disciplina",
                column: "DisciplinaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_MatriculaModelNumeroDeMatricula",
                table: "disciplina",
                column: "MatriculaModelNumeroDeMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_ProfessorId",
                table: "disciplina",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_professor_USING professor_id::integer",
                table: "professor",
                column: "USING professor_id::integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aluno_disciplina");

            migrationBuilder.DropTable(
                name: "pessoa_admin");

            migrationBuilder.DropTable(
                name: "disciplina");

            migrationBuilder.DropTable(
                name: "aluno");

            migrationBuilder.DropTable(
                name: "professor");

            migrationBuilder.DropTable(
                name: "matricula");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "curriculo");

            migrationBuilder.DropTable(
                name: "curso");
        }
    }
}
