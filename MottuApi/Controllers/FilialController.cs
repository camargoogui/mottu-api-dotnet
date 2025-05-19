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
            if (filial == null) return NotFound("Filial não encontrada");
            return Ok(_mapper.Map<FilialDto>(filial));
        }

        [HttpPost]
        public async Task<ActionResult<FilialDto>> Create(FilialDto dto)
        {
            try
            {
                var filial = new Filial(dto.Nome);
                var created = await _service.AddAsync(filial);
                var result = _mapper.Map<FilialDto>(created);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FilialDto dto)
        {
            if (id != dto.Id) return BadRequest("ID da URL difere do corpo da requisição");

            var filial = await _service.GetByIdAsync(id);
            if (filial == null) return NotFound("Filial não encontrada");

            try
            {
                filial.SetNome(dto.Nome);
                var updated = await _service.UpdateAsync(filial);
                if (!updated) return BadRequest("Erro ao atualizar a filial");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound("Filial não encontrada");
            return NoContent();
        }
    }
}
