using AutoMapper;
using sme.src.Models;
using sme.src.Models.Abstract;
using sme.src.Models.Empresa;
using sme.src.Public.DTOs;

namespace sme.src.Public.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            //TODO: profile de usuário para o restante das entidades (professor, aluno, empresa)
            CreateMap<AlunoCreationRequest, Usuario>();
            CreateMap<ProfessorCreationRequest, Usuario>();
            CreateMap<EmpresaParceiraCreationRequest, Usuario>();
        }
    }
}