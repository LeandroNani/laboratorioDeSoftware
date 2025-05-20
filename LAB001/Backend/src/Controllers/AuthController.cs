using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.Mailer;
using Backend.src.models;
using Backend.src.services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.controllers
{
    [ApiController]
    [Route("[controller]")] // Define a rota como /auth
    public class AuthController(AppDbContext context) : ControllerBase
    {
        private readonly AuthService _authService = new(context);

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginRequest">Numero de pessoa e senha</param>
        /// <returns>Retorna os dados da pessoa que efetuou o login</returns>
        /// <response code="200">Login efetuado com sucesso</response>
        /// <response code="401">Credenciais incorretas</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(PessoaModel), 200)]
        [ProducesResponseType(typeof(AlunoModel), 200)]
        [ProducesResponseType(typeof(ProfessorModel), 200)]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var pessoa = await _authService.Login(loginRequest);
            return Ok(pessoa);
        }

        /// <summary>
        /// Cria um perfil de administrador
        /// </summary>
        /// <param name="admin">Dados do admin</param>
        /// <response code="200">Adminstrador criado</response>
        [HttpPost("admin")]
        public async Task<IActionResult> CreateAdmin(AdminModel admin)
        {
            await _authService.CreateAdmin(admin);
            return Created();
        }
    }
}
