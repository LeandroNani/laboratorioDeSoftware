using System.Net;
using Microsoft.AspNetCore.Mvc;
using sme.src.Data;
using sme.src.Models.Empresa;
using sme.src.Services;

namespace sme.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController(AppDbContext context) : ControllerBase
    {
        private readonly Service<Produto> _service = new(context);

        [HttpGet("get/{id}")]
        [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _service.GetByIdAsync(id);
            return Ok(produto);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<Produto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _service.GetAllAsync();
            return Ok(produtos);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] Produto produto)
        {
            var creation = await _service.AddAsync(produto);
            return CreatedAtAction(nameof(GetById), new { id = creation.Id }, creation);
        }

        [HttpPut("put/{id}")]
        [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] Produto produto)
        {
            var updatedProduto = await _service.UpdateAsync(produto, id);
            return Ok(updatedProduto);
        }

        [HttpDelete("del/{id}")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}