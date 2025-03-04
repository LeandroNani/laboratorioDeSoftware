using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;

namespace Backend.src.services.Auth
{
    public class AuthService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<object> Login(LoginRequest loginRequest)
        {
            var pessoa = await _context.Pessoas.FindAsync(loginRequest.NumeroDePessoa);
            if (
                pessoa != null
                && pessoa.NumeroDePessoa.Equals(loginRequest.NumeroDePessoa)
                && pessoa.Senha.Equals(loginRequest.Senha)
            )
            {
                return pessoa;
            }
            else
            {
                throw new UnauthorizedException("Credenciais incorretas");
            }
        }

        public async Task<AdminModel> FindAdminByNumero(int NumeroDePessoa)
        {
            AdminModel admin =
                await _context.Admins.FindAsync(NumeroDePessoa)
                ?? throw new NotFoundException(
                    $"Admin com o número de pessoa {NumeroDePessoa} não encontrado"
                );
            return admin;
        }
    }
}
