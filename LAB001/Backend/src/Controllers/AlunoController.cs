using Backend.src.Data;
using Backend.src.DTOs;
using Backend.src.Middlewares.Exceptions;
using Backend.src.models;
using Backend.src.services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.src.controllers
{
    [ApiController]
    [Route("[controller]")] // Define a rota como /aluno
    public class AlunoController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly AlunoService _alunoService = new(context);

        /// <summary>
        /// Adiciona um novo aluno.
        /// </summary>
        /// <param name="aluno">Dados do aluno a ser adicionado</param>
        /// <returns>Retorna o aluno adicionado</returns>
        /// <response code="200">Aluno adicionado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost("adicionar")]
        [ProducesResponseType(typeof(AlunoModel), 200)]
        public async Task<IActionResult> AdicionarAluno(AlunoModel aluno)
        {
            await _alunoService.AdicionarAluno(aluno);
            return Ok(aluno);
        }

        /// <summary>
        /// Obtém o preço do semestre para um aluno específico.
        /// </summary>
        /// <param name="numeroDePessoa">Número identificador da pessoa</param>
        /// <returns>Retorna o preço do semestre do aluno</returns>
        /// <response code="200">Retorna o preço do semestre</response>
        /// <response code="404">Se o aluno não for encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet("preco-semestre/{numeroDePessoa}")]
        [ProducesResponseType(typeof(ResponsePrecoSemestre), 200)]
        [SwaggerOperation(
            Summary = "Obtém o preço do semestre de um aluno",
            Description = "Retorna o preço do semestre baseado no número da pessoa."
        )]
        public async Task<IActionResult> GetPrecoSemestre(int numeroDePessoa)
        {
            ResponsePrecoSemestre response = await _alunoService.GetPrecoSemestre(numeroDePessoa);
            return Ok(response);
        }

        /// <summary>
        /// Cancela a matrícula de um aluno.
        /// </summary>
        /// <param name="cancelarMatriculaRequest">Dados necessários para cancelar a matrícula</param>
        /// <returns>Retorna o aluno com a matrícula cancelada</returns>
        /// <response code="200">Matrícula cancelada com sucesso</response>
        /// <response code="404">Aluno não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPatch("cancelar-matricula")]
        [ProducesResponseType(typeof(AlunoModel), 200)]
        public async Task<IActionResult> CancelarMatricula(
            CancelarMatriculaRequest cancelarMatriculaRequest
        )
        {
            AlunoModel aluno = await _alunoService.CancelarMatricula(cancelarMatriculaRequest);
            return Ok(aluno);
        }

        /// <summary>
        /// Lista todos os alunos.
        /// </summary>
        /// <returns>Retorna uma lista de alunos</returns>
        /// <response code="200">Lista de alunos retornada com sucesso</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet("listar-alunos")]
        [ProducesResponseType(typeof(List<AlunoModel>), 200)]
        public async Task<IActionResult> ListarAlunos()
        {
            List<AlunoModel> alunos = await _alunoService.ListarAlunos();
            return Ok(alunos);
        }

        /// <summary>
        /// Obtém os dados de um aluno específico.
        /// </summary>
        /// <param name="numeroDePessoa">Número identificador da pessoa</param>
        /// <returns>Retorna os dados do aluno</returns>
        /// <response code="200">Retorna os dados do aluno</response>
        /// <response code="404">Se o aluno não for encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet("get-aluno/{numeroDePessoa}")]
        [ProducesResponseType(typeof(List<AlunoModel>), 200)]
        public async Task<IActionResult> GetAluno(int numeroDePessoa)
        {
            AlunoModel aluno = await _alunoService.GetAlunoByNumeroDePessoa(numeroDePessoa);
            return Ok(aluno);
        }
    }
}
