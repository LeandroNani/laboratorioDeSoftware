using System.ComponentModel.DataAnnotations;
using Backend.src.models;

namespace Backend.src.DTOs
{
    public class EfetuarMatriculaRequest
    {
        [Required]
        public required List<DisciplinaModel> Disciplinas;

        [Required]
        public required int NumeroDePessoa;
    }
}
