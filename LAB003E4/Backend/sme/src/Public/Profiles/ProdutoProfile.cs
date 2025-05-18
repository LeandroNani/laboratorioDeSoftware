using AutoMapper;
using sme.src.Models.Empresa;
using sme.src.Public.DTOs;
namespace sme.src.Public.Profiles
{

    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<ProdutoCreationRequest, Produto>();
            CreateMap<ProdutoUpdateRequest, Produto>();
            CreateMap<Produto, ProdutoResponse>()
                .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.Empresa));
        }
    }
}