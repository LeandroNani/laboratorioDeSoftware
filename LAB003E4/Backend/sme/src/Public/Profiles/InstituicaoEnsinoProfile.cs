using AutoMapper;
using sme.src.Models;
using sme.src.Models.Empresa;
using sme.src.Public.DTOs;

namespace sme.src.Public.Profiles
{

    public class InstituicaoEnsinoProfile : Profile
    {
        public InstituicaoEnsinoProfile()
        {
            CreateMap<InstituicaoEnsinoCreationRequest, InstituicaoEnsino>();
            CreateMap<InstituicaoEnsinoUpdateRequest, InstituicaoEnsino>();
        }
    }
}