using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sme.src.Data;
using sme.src.Models;
using sme.src.Public.DTOs;
using sme.src.Services;

namespace sme.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController(
        Service<Professor> crudService,
        ProfessorService profService,
        IMapper mapper) : ControllerBase
    {
        private readonly Service<Professor> _crudService = crudService;
        private readonly ProfessorService _profService = profService;
        private readonly IMapper _mapper = mapper;

        // GET api/professor/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProfessorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProfessorResponse>> GetById(int id)
        {
            var prof = await _crudService.GetByIdAsync(id);
            var resp = _mapper.Map<ProfessorResponse>(prof);
            return Ok(resp);
        }

        // GET api/professor
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProfessorResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProfessorResponse>>> GetAll()
        {
            var list = await _crudService.GetAllAsync();
            var resp = _mapper.Map<IEnumerable<ProfessorResponse>>(list);
            return Ok(resp);
        }

        // POST api/professor
        [HttpPost]
        [ProducesResponseType(typeof(ProfessorResponse), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ProfessorResponse>> Create([FromBody] ProfessorCreationRequest request)
        {
            var prof = await _profService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), new { prof });
        }

        // PUT api/professor/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProfessorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProfessorResponse>> Update(int id, [FromBody] ProfessorUpdateRequest request)
        {
            var prof = await _profService.UpdateAsync(id, request);
            var resp = _mapper.Map<ProfessorResponse>(prof);
            return Ok(resp);
        }

        // POST api/professor/{professorId}/coins
        [HttpPost("{professorId}/coins")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> SendCoins(int professorId, [FromBody] CoinTransferRequest request)
        {
            await _profService.SendCoinsAsync(professorId, request);
            return Ok();
        }
    }
}
