namespace SistemaDeMatriculas.Models

{
    public class ProfessorModel : PessoaModel
    {
        public required DisciplinaModel[] Disciplinas { get; set; }
        public required string NivelEscolar { get; set; }
        public ProfessorModel()
        {
        }
    }
}