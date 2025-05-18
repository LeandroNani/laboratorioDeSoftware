using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using sme.src.Data;
using sme.src.Models;
using sme.src.Public.DTOs;
using sme.src.Services;

namespace sme.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly Service<Professor> _service;
        private readonly ProfessorService _profService;
        private readonly IMapper _mapper;

        public ProfessorController(AppDbContext context, IMapper mapper)
        {
            _service = new Service<Professor>(context);
            _profService = new ProfessorService(context, mapper);
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProfessorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var prof = await _service.GetByIdAsync(id);
            var resp = _mapper.Map<ProfessorResponse>(prof);
            return Ok(resp);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<ProfessorResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            var resp = _mapper.Map<IEnumerable<ProfessorResponse>>(list);
            return Ok(resp);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(ProfessorResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] ProfessorCreationRequest request)
        {
            var prof = await _profService.CreateAsync(request);
            var resp = _mapper.Map<ProfessorResponse>(prof);
            return CreatedAtAction(nameof(GetById), new { id = prof.Id }, resp);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(ProfessorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] ProfessorUpdateRequest request)
        {
            var prof = await _profService.UpdateAsync(id, request);
            var resp = _mapper.Map<ProfessorResponse>(prof);
            return Ok(resp);
        }

        [HttpPost("sendCoins/{professorId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> SendCoins(int professorId, [FromBody] CoinTransferRequest request)
        {
            await _profService.SendCoinsAsync(professorId, request);
            return Ok();
        }

        [HttpPost("allocateSemesterCoins")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AllocateSemesterCoins()
        {
            await _profService.AllocateSemesterCoinsAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
