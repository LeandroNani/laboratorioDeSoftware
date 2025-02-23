namespace SistemaDeMatriculas.Models

{
	public class CursoModel
	{
		public required string Id { get; set; }
		public required string Nome { get; set; }
		public required DisciplinaModel[] Disciplinas { get; set; }
		public required AlunoModel[] Alunos { get; set; }
		public required int NumeroDeCreditos { get; set; }

		public CursoModel()
		{
		}
	}
}