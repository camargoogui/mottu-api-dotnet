using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MottuApi.Application.DTOs;
using MottuApi.Application.Interfaces;
using MottuApi.Domain.Exceptions;

namespace MottuApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly IMotoService _motoService;

        public MotoController(IMotoService motoService)
        {
            _motoService = motoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotoDTO>>> GetAll()
        {
            try
            {
                var motos = await _motoService.GetAllAsync();
                return Ok(motos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MotoDTO>> GetById(int id)
        {
            try
            {
                var moto = await _motoService.GetByIdAsync(id);
                return Ok(moto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("por-placa")]
        public async Task<ActionResult<MotoDTO>> GetByPlaca([FromQuery] string placa)
        {
            try
            {
                var moto = await _motoService.GetByPlacaAsync(placa);
                return Ok(moto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("por-filial/{filialId}")]
        public async Task<ActionResult<IEnumerable<MotoDTO>>> GetByFilialId(int filialId)
        {
            try
            {
                var motos = await _motoService.GetByFilialIdAsync(filialId);
                return Ok(motos);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<MotoDTO>> Create([FromBody] CreateMotoDTO createMotoDTO)
        {
            try
            {
                var moto = await _motoService.CreateAsync(createMotoDTO);
                return CreatedAtAction(nameof(GetById), new { id = moto.Id }, moto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MotoDTO>> Update(int id, [FromBody] UpdateMotoDTO updateMotoDTO)
        {
            try
            {
                var moto = await _motoService.UpdateAsync(id, updateMotoDTO);
                return Ok(moto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _motoService.DeleteAsync(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPatch("{id}/disponivel")]
        public async Task<ActionResult<MotoDTO>> MarcarComoDisponivel(int id)
        {
            try
            {
                var moto = await _motoService.MarcarComoDisponivelAsync(id);
                return Ok(moto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpPatch("{id}/indisponivel")]
        public async Task<ActionResult<MotoDTO>> MarcarComoIndisponivel(int id)
        {
            try
            {
                var moto = await _motoService.MarcarComoIndisponivelAsync(id);
                return Ok(moto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
