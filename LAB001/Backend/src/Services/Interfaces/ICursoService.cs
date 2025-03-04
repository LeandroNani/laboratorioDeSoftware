using Backend.src.DTOs;
using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface ICursoService
    {
        CursoModel CriarCurso(CursoModel curso);
    }
}
