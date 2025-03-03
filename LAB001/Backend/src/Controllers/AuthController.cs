using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.models;
using Backend.src.services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.controllers
{
    [ApiController]
    [Route("[controller]")] // Define a rota como /auth
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
            _authService = new AuthService(_context);
        }

        private readonly AuthService _authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var pessoa = await _authService.Login(loginRequest);
            return Ok(pessoa);
        }
    }
}
