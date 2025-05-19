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
    public class FilialController : ControllerBase
    {
        private readonly IFilialService _filialService;

        public FilialController(IFilialService filialService)
        {
            _filialService = filialService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilialDTO>>> GetAll()
        {
            try
            {
                var filiais = await _filialService.GetAllAsync();
                return Ok(filiais);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilialDTO>> GetById(int id)
        {
            try
            {
                var filial = await _filialService.GetByIdAsync(id);
                return Ok(filial);
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
        public async Task<ActionResult<FilialDTO>> Create([FromBody] CreateFilialDTO createFilialDTO)
        {
            try
            {
                var filial = await _filialService.CreateAsync(createFilialDTO);
                return CreatedAtAction(nameof(GetById), new { id = filial.Id }, filial);
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
        public async Task<ActionResult<FilialDTO>> Update(int id, [FromBody] UpdateFilialDTO updateFilialDTO)
        {
            try
            {
                var filial = await _filialService.UpdateAsync(id, updateFilialDTO);
                return Ok(filial);
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
                await _filialService.DeleteAsync(id);
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
    }
} 