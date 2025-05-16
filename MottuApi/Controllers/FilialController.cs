using Microsoft.AspNetCore.Mvc;
using MottuApi.DTOs;
using MottuApi.Models;
using AutoMapper;
using MottuApi.Services;

namespace MottuApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilialController : ControllerBase
    {
        private readonly FilialService _service;
        private readonly IMapper _mapper;

        public FilialController(FilialService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilialDto>>> GetAll()
        {
            var filiais = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<FilialDto>>(filiais));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilialDto>> GetById(int id)
        {
            var filial = await _service.GetByIdAsync(id);
            if (filial == null) return NotFound();
            return Ok(_mapper.Map<FilialDto>(filial));
        }

        [HttpPost]
        public async Task<ActionResult<FilialDto>> Create(FilialDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome)) return BadRequest();
            var filial = _mapper.Map<Filial>(dto);
            var created = await _service.AddAsync(filial);
            var result = _mapper.Map<FilialDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FilialDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var filial = await _service.GetByIdAsync(id);
            if (filial == null) return NotFound();
            filial.Nome = dto.Nome;
            var updated = await _service.UpdateAsync(filial);
            if (!updated) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
