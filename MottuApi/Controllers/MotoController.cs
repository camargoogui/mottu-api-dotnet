// Controllers/MotoController.cs
using Microsoft.AspNetCore.Mvc;
using MottuApi.DTOs;
using MottuApi.Models;

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
        if (moto == null) return NotFound();
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
        if (string.IsNullOrWhiteSpace(dto.Placa) || string.IsNullOrWhiteSpace(dto.Modelo)) return BadRequest();
        var filial = await _filialService.GetByIdAsync(dto.FilialId);
        if (filial == null) return BadRequest("Filial não encontrada");
        var moto = _mapper.Map<Moto>(dto);
        var created = await _service.AddAsync(moto);
        var result = _mapper.Map<MotoDto>(created);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MotoDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var moto = await _service.GetByIdAsync(id);
        if (moto == null) return NotFound();
        moto.Placa = dto.Placa;
        moto.Modelo = dto.Modelo;
        moto.FilialId = dto.FilialId;
        var updated = await _service.UpdateAsync(moto);
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
