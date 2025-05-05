using sme.src.Models;
using sme.src.Public.DTOs;

namespace sme.src.Public.Profiles
{
    public class AlunoProfile : AutoMapper.Profile
    {
        public AlunoProfile()
        {
            CreateMap<AlunoCreationRequest, Aluno>();
            CreateMap<AlunoUpdateRequest, Aluno>();
            CreateMap<Aluno, AlunoResponse>()
                .ForMember(dest => dest.Moedas, opt => opt.MapFrom(src => src.Moedas))
                .ForMember(dest => dest.Curso, opt => opt.MapFrom(src => src.Curso))
                .ForMember(dest => dest.Instituicao, opt => opt.MapFrom(src => src.Instituicao));
        }
    }
}