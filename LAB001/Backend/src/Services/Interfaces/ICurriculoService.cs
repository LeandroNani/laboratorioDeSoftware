using Backend.src.DTOs;
using Backend.src.models;

namespace Backend.src.services.interfaces
{
    public interface ICurriculoService
    {
        Task<CurriculoModel> CriarCurriculo(CurriculoModel curriculo);
        Task<List<CurriculoModel>> ListarCurriculos();
    }
}
