using Backend.src.models;

namespace Backend.src.DTOs
{
    public class AlocarDisciplinaRequest
    {
        public required DisciplinaModel Disciplina;
        public required string NumeroDePessoa;
    }

    public class RemoverProfessor
    {
        public required string NumeroDePessoa;
    }
}
