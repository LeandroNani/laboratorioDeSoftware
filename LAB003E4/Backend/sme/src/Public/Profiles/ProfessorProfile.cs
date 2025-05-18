using AutoMapper;
using sme.src.Models;
using sme.src.Models.Empresa;
using sme.src.Public.DTOs;

namespace sme.src.Public.Profiles
{

    public class ProfessorProfile : Profile
    {
        public ProfessorProfile()
        {
            CreateMap<ProfessorUpdateRequest, Professor>();
            CreateMap<Professor, ProfessorResponse>()
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.Departamento));
        }
    }
}