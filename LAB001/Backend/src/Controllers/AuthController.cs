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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var pessoa = await _authService.Login(loginRequest);
            return Ok(pessoa);
        }

        [HttpPost("admin")]
        public async Task<IActionResult> CreateAdmin(AdminModel admin)
        {
            await _authService.CreateAdmin(admin);
            return Created();
        }
    }
}
