using System.Threading.Tasks;
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
            var pessoa =
                await _context.Pessoas.FindAsync(loginRequest.NumeroDePessoa)
                ?? throw new NotFoundException(
                    "Este número de pessoa ainda não foi cadastrado, entre em contato com a secretaria"
                );
            if (
                pessoa.NumeroDePessoa.Equals(loginRequest.NumeroDePessoa)
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

        public async Task<AdminModel> FindAdminByNumero(string NumeroDePessoa)
        {
            AdminModel admin =
                await _context.Admins.FindAsync(NumeroDePessoa)
                ?? throw new NotFoundException(
                    $"Admin com o número de pessoa {NumeroDePessoa} não encontrado"
                );
            return admin;
        }

        public async Task CreateAdmin(AdminModel admin)
        {
            AdminModel? exisitingAdmin = await _context.Admins.FindAsync(admin.NumeroDePessoa);
            if (exisitingAdmin != null)
            {
                throw new Middlewares.Exceptions.InvalidOperationException(
                    $"Admin com numero de pessoa {admin.NumeroDePessoa} ja existe"
                );
            }
            _context.Admins.Add(admin);
        }
    }
}
