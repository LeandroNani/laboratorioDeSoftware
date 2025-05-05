using AutoMapper;
using sme.src.Models.Empresa;
using sme.src.Public.DTOs;

namespace sme.src.Public.Profiles
{

    public class EmpresaParceiraProfile : Profile
    {
        public EmpresaParceiraProfile()
        {
            CreateMap<EmpresaParceiraCreationRequest, EmpresaParceira>();
            CreateMap<EmpresaParceiraUpdateRequest, EmpresaParceira>();
            CreateMap<EmpresaParceira, EmpresaParceiraResponse>();
        }
    }
}