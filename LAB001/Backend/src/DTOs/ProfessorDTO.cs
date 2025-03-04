using Backend.src.models;

namespace Backend.src.DTOs
{
    public class AlocarDisciplinaRequest
    {
        public required DisciplinaModel Disciplina;
        public required int NumeroDePessoa;
    }

    public class RemoverProfessor
    {
        public required int NumeroDePessoa;
    }
}
