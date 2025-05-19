using Microsoft.AspNetCore.Mvc;
using MottuApi.DTOs;
using MottuApi.Models;
using AutoMapper;
using MottuApi.Services;

namespace MottuApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly MotoService _service;
        private readonly FilialService _filialService;
        private readonly IMapper _mapper;

        public MotoController(MotoService service, FilialService filialService, IMapper mapper)
        {
            _service = service;
            _filialService = filialService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotoDto>>> GetAll()
        {
            var motos = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<MotoDto>>(motos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MotoDto>> GetById(int id)
        {
            var moto = await _service.GetByIdAsync(id);
            if (moto == null) return NotFound("Moto não encontrada");
            return Ok(_mapper.Map<MotoDto>(moto));
        }

        [HttpGet("por-filial/{filialId}")]
        public async Task<ActionResult<IEnumerable<MotoDto>>> GetByFilial(int filialId)
        {
            var motos = await _service.GetAllAsync();
            var filtradas = motos.Where(m => m.FilialId == filialId);
            return Ok(_mapper.Map<IEnumerable<MotoDto>>(filtradas));
        }

        [HttpGet("por-placa")]
        public async Task<ActionResult<IEnumerable<MotoDto>>> GetByPlaca([FromQuery] string placa)
        {
            var motos = await _service.GetAllAsync();
            var filtradas = motos.Where(m => m.Placa.ToLower() == placa.ToLower());
            return Ok(_mapper.Map<IEnumerable<MotoDto>>(filtradas));
        }

        [HttpPost]
        public async Task<ActionResult<MotoDto>> Create(MotoDto dto)
        {
            var filial = await _filialService.GetByIdAsync(dto.FilialId);
            if (filial == null) return BadRequest("Filial não encontrada");

            try
            {
                var moto = new Moto(dto.Placa, dto.Modelo, dto.FilialId);
                var created = await _service.AddAsync(moto);
                var result = _mapper.Map<MotoDto>(created);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MotoDto dto)
        {
            if (id != dto.Id) return BadRequest("ID da URL difere do corpo da requisição");

            var moto = await _service.GetByIdAsync(id);
            if (moto == null) return NotFound("Moto não encontrada");

            try
            {
                moto.SetPlaca(dto.Placa);
                moto.SetModelo(dto.Modelo);
                moto.SetFilial(dto.FilialId);
                var updated = await _service.UpdateAsync(moto);
                if (!updated) return BadRequest("Erro ao atualizar a moto");
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
            if (!deleted) return NotFound("Moto não encontrada");
            return NoContent();
        }
    }
}
