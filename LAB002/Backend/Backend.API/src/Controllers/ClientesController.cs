using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Backend.API.Data;         
using Backend.API.Model;         

namespace Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra um novo cliente.
        /// </summary>
        /// <param name="req">Dados do cliente</param>
        /// <returns>Mensagem de sucesso ou erro</returns>
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarCliente([FromBody] RegistrarClienteRequest req)
        {
            bool existe = await _context.Clientes.AnyAsync(c => c.Email == req.Email);
            if (existe)
                return BadRequest("Já existe um cliente com esse e-mail.");

            var cliente = new Cliente
            {
                Nome = req.Nome,
                Email = req.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(req.Senha),
                RG = req.RG,
                CPF = req.CPF,
                Endereco = req.Endereco,
                Profissao = req.Profissao,
                EntidadeEmpregadora = req.EntidadeEmpregadora
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Ok($"Cliente cadastrado com ID {cliente.Id}");
        }
    }

    public class RegistrarClienteRequest
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string RG { get; set; }
        public required string CPF { get; set; }
        public required string Endereco { get; set; }
        public required string Profissao { get; set; }
        public required string EntidadeEmpregadora { get; set; }
    }
}
