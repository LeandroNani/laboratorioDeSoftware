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
        private readonly Service<EmpresaParceira> _service = new(context);

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
        public async Task<IActionResult> Update(int id, [FromBody] EmpresaParceiraUpdateRequest empresaParceira)
        {
            var empresa = _mapper.Map<EmpresaParceira>(empresaParceira);
            var e = await _service.UpdateAsync(empresa, id);
            return Ok(e);
        }

        [HttpDelete("del/{id}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("produtos/{id}")]
        [ProducesResponseType(typeof(IEnumerable<Produto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProdutosByEmpresaId(int id)
        {
            var _service = new Service<Produto>(context);
            var produtos = await _service.GetAllAsync();
            var produtosEmpresa = produtos.Where(p => p.Empresa.Id == id).ToList();
            return produtosEmpresa.Count != 0
                ? Ok(produtosEmpresa) 
                : NotFound($"Nenhum produto encontrado para a empresa com ID {id}.");
        }
    }
}
