using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sme.src.Models;
using sme.src.Middlewares;
using sme.src.Data;
using sme.src.Services;
using sme.src.Models.Empresa;
using sme.src.Public.DTOs;
using AutoMapper;

namespace sme.src.Controllers
{

    /// <summary>
    /// Controlador responsável por gerenciar as operações relacionadas às empresas parceiras.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaParceiraController(AppDbContext context, IMapper _mapper) : ControllerBase
    {
       private readonly Service<EmpresaParceira> _service = new (context);

        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(EmpresaParceira), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var empresaParceira = await _service.GetByIdAsync(id);
            return Ok(empresaParceira);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<EmpresaParceira>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var empresasParceiras = await _service.GetAllAsync();
            return Ok(empresasParceiras);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(EmpresaParceira), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] EmpresaParceiraCreationRequest _empresaParceira)
        {
            var empresaParceira = _mapper.Map<EmpresaParceira>(_empresaParceira);
            var creation = await _service.AddAsync(empresaParceira);
            return CreatedAtAction(nameof(GetById), new { id = creation.Id }, creation);
        }

        [HttpPut("put/{id}")]
        [ProducesResponseType(typeof(EmpresaParceira), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] EmpresaParceira empresaParceira)
        {
            await _service.UpdateAsync(empresaParceira);
            return Ok(empresaParceira);
        }

        [HttpDelete("del/{id}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        
    }
}
