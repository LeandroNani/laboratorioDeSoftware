using AutoMapper;
using sme.src.Models;
using sme.src.Models.Empresa;
using sme.src.Public.DTOs;

namespace sme.src.Public.Profiles
{
    public class CursoProfile : Profile
    {
        public CursoProfile()
        {
            CreateMap<CursoCreationRequest, Curso>();
            CreateMap<CursoUpdateRequest, Curso>();
        }
    }
}