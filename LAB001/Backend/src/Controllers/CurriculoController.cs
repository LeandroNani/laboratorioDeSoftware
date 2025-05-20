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

        /// <summary>
        /// Cria um novo curriculo
        /// </summary>
        /// <param name="curriculo">Dados do curriculo</param>
        /// <returns>O novo curriculo criado</returns>
        /// <response code="200">Curriculo criado com sucesso</response>
        /// <response code="500">Erro interno</response>
        [HttpPost("novo-curriculo")]
        [ProducesResponseType(typeof(CurriculoModel), 200)]
        public async Task<IActionResult> CriarCurriculo(CurriculoModel curriculo)
        {
            await _curriculoService.CriarCurriculo(curriculo);
            return Ok(curriculo);
        }

        /// <summary>
        /// Cria um novo curriculo
        /// </summary>
        /// <returns>Busca o curriculo</returns>
        /// <response code="200">Curriculo procurado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("get-curriculo")]
        [ProducesResponseType(typeof(CurriculoModel), 200)]
        public async Task<IActionResult> GetCurriculo()
        {
            CurriculoModel curriculo = await _curriculoService.GetCurriculo();
            return Ok(curriculo);
        }

        /// <summary>
        /// Atualiza um curriculo
        /// </summary>
        /// <param name="curriculo">Dados do curriculo</param>
        /// <returns>O curriculo atualizado</returns>
        /// <response code="200">Curriculo atualizado com sucesso</response>
        /// <response code="500">Erro interno</response>
        [HttpPut("atualizar-curriculo")]
        [ProducesResponseType(typeof(CurriculoModel), 200)]
        public async Task<IActionResult> UpdateCurriculo(CurriculoModel curriculo)
        {
            await _curriculoService.UpdateCurriculo(curriculo);
            return Ok(curriculo);
        }
    }
}
