namespace sme.src.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using EcoScale.src.Auth;
    using Microsoft.EntityFrameworkCore;
    using sme.src.Auth;
    using sme.src.Data;
    using sme.src.Middlewares.Exceptions;
    using sme.src.Models.Empresa;
    using sme.src.Public.DTOs;

    public class EmpresaService(IMapper mapper, AppDbContext context)
    {
        private readonly IMapper _mapper = mapper;
        private readonly AppDbContext _context = context;
        private readonly Jwt _jwt = new(context);

        public async Task<CreationResponse<EmpresaParceiraResponse>> CreateEmpresaAsync(EmpresaParceiraCreationRequest request)
        {
            var empresaParceira = _mapper.Map<EmpresaParceira>(request);
            var existingEmpresa = await _context.EmpresasParceiras.AnyAsync(e => e.Cnpj == empresaParceira.Cnpj || e.Email == empresaParceira.Email)
                ? throw new ConflictException("Já existe uma empresa parceira cadastrada com o mesmo CNPJ ou email.") : false;

            await _context.SaveChangesAsync();
            return new CreationResponse<EmpresaParceiraResponse>
            {
                Entity = _mapper.Map<EmpresaParceiraResponse>(empresaParceira),
                Token = _jwt.GenerateToken(empresaParceira.Email, Role.Empresa)
            };
        }
    }
}