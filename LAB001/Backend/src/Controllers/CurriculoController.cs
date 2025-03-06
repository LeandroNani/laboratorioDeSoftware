using System.Threading.Tasks;
using Backend.src.Data;
using Backend.src.models;
using Backend.src.services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurriculoController(AppDbContext context) : ControllerBase
    {
        private readonly CurriculoService _curriculoService = new(context);

        [HttpPost("novo-curriculo")]
        public async Task<IActionResult> CriarCurriculo(CurriculoModel curriculo)
        {
            await _curriculoService.CriarCurriculo(curriculo);
            return Ok(curriculo);
        }

        [HttpGet("get-curriculo")]
        public async Task<IActionResult> GetCurriculo()
        {
            CurriculoModel curriculo = await _curriculoService.GetCurriculo();
            return Ok(curriculo);
        }

        [HttpPut("atualizar-curriculo")]
        public async Task<IActionResult> UpdateCurriculo(CurriculoModel curriculo)
        {
            await _curriculoService.UpdateCurriculo(curriculo);
            return Ok(curriculo);
        }
    }
}
