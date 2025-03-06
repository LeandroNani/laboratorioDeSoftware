using System.ComponentModel.DataAnnotations;
using Backend.src.models;

namespace Backend.src.DTOs
{
    public class EfetuarMatriculaRequest
    {
        [Required]
        public required List<DisciplinaModel> disciplinas;

        [Required]
        public required string numeroDePessoa;
    }

    public class GetPrecoSemestre
    {
        [Required]
        public int NumeroDePessoa;
    }

    public class ResponsePrecoSemestre
    {
        [Required]
        public int Preco;
    }

    public class RemoverAlunoRequest
    {
        [Required]
        public required string NumeroDePessoa;
    }
}
