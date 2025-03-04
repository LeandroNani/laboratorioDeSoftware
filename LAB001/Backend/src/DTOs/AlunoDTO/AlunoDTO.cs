using System.ComponentModel.DataAnnotations;
using Backend.src.models;

namespace Backend.src.DTOs.AlunoDTO
{
    public class EfetuarMatriculaRequest
    {
        [Required]
        public required List<DisciplinaModel> Disciplinas;

        [Required]
        public required int NumeroDePessoa;
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
        public int NumeroDePessoa;
    }
}
