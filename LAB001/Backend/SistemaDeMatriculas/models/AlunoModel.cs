using System;
using SistemaDeMatriculas.Models;
namespace SistemaDeMatriculas.Models
{
    public class AlunoModel : PessoaModel
    {
        public required string Id { get; set; }
        public required CursoModel Curso { get; set; }
        public required string Matricula { get; set; }
        public required DisciplinaModel[] DisciplinasFeitas { get; set; }
        public required string Email { get; set; }
        public required int Mensalidade { get; set; }


        public AlunoModel()
        {
        }
    }
}
