using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MottuApi.Application.DTOs;
using MottuApi.Application.Interfaces;
using MottuApi.Domain.Exceptions;

namespace MottuApi.Presentation.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de filiais da Mottu
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FilialController : ControllerBase
    {
        private readonly IFilialService _filialService;

        public FilialController(IFilialService filialService)
        {
            _filialService = filialService;
        }

        /// <summary>
        /// Lista todas as filiais com paginação
        /// </summary>
        /// <param name="page">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10, máximo: 100)</param>
        /// <returns>Lista paginada de filiais</returns>
        /// <response code="200">Retorna a lista paginada de filiais</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDTO<FilialDTO>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PagedResultDTO<FilialDTO>>> GetAll(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 10;

                var result = await _filialService.GetAllPagedAsync(page, pageSize);
                return Ok(result);
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
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

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
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

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

        [HttpPatch("{id}/ativar")]
        public async Task<ActionResult<FilialDTO>> Ativar(int id)
        {
            try
            {
                var filial = await _filialService.AtivarAsync(id);
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

        [HttpPatch("{id}/desativar")]
        public async Task<ActionResult<FilialDTO>> Desativar(int id)
        {
            try
            {
                var filial = await _filialService.DesativarAsync(id);
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
    }
}
