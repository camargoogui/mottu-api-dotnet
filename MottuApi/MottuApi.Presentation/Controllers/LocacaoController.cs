using Microsoft.AspNetCore.Mvc;
using MottuApi.Application.DTOs;
using MottuApi.Application.Interfaces;

namespace MottuApi.Presentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LocacaoController : ControllerBase
    {
        private readonly ILocacaoService _locacaoService;

        public LocacaoController(ILocacaoService locacaoService)
        {
            _locacaoService = locacaoService;
        }

        /// <summary>
        /// Lista todas as locações com paginação
        /// </summary>
        /// <param name="page">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10)</param>
        /// <returns>Lista paginada de locações</returns>
        [HttpGet]
        public async Task<ActionResult<PagedResultDTO<LocacaoDTO>>> GetAll(int page = 1, int pageSize = 10)
        {
            var locacoes = await _locacaoService.GetAllAsync(page, pageSize);
            return Ok(locacoes);
        }

        /// <summary>
        /// Busca uma locação por ID
        /// </summary>
        /// <param name="id">ID da locação</param>
        /// <returns>Dados da locação</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LocacaoDTO>> GetById(int id)
        {
            var locacao = await _locacaoService.GetByIdAsync(id);
            if (locacao == null)
                return NotFound();

            return Ok(locacao);
        }

        /// <summary>
        /// Lista locações de uma moto específica
        /// </summary>
        /// <param name="motoId">ID da moto</param>
        /// <returns>Lista de locações da moto</returns>
        [HttpGet("por-moto/{motoId}")]
        public async Task<ActionResult<IEnumerable<LocacaoDTO>>> GetByMotoId(int motoId)
        {
            var locacoes = await _locacaoService.GetByMotoIdAsync(motoId);
            return Ok(locacoes);
        }

        /// <summary>
        /// Lista locações de uma filial específica
        /// </summary>
        /// <param name="filialId">ID da filial</param>
        /// <returns>Lista de locações da filial</returns>
        [HttpGet("por-filial/{filialId}")]
        public async Task<ActionResult<IEnumerable<LocacaoDTO>>> GetByFilialId(int filialId)
        {
            var locacoes = await _locacaoService.GetByFilialIdAsync(filialId);
            return Ok(locacoes);
        }

        /// <summary>
        /// Busca locações por CPF do cliente
        /// </summary>
        /// <param name="cpf">CPF do cliente</param>
        /// <returns>Lista de locações do cliente</returns>
        [HttpGet("por-cliente")]
        public async Task<ActionResult<IEnumerable<LocacaoDTO>>> GetByClienteCpf([FromQuery] string cpf)
        {
            var locacoes = await _locacaoService.GetByClienteCpfAsync(cpf);
            return Ok(locacoes);
        }

        /// <summary>
        /// Busca locações por período
        /// </summary>
        /// <param name="inicio">Data de início</param>
        /// <param name="fim">Data de fim</param>
        /// <returns>Lista de locações no período</returns>
        [HttpGet("por-periodo")]
        public async Task<ActionResult<IEnumerable<LocacaoDTO>>> GetByPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            var locacoes = await _locacaoService.GetByPeriodoAsync(inicio, fim);
            return Ok(locacoes);
        }

        /// <summary>
        /// Lista locações ativas
        /// </summary>
        /// <returns>Lista de locações ativas</returns>
        [HttpGet("ativas")]
        public async Task<ActionResult<IEnumerable<LocacaoDTO>>> GetAtivas()
        {
            var locacoes = await _locacaoService.GetAtivasAsync();
            return Ok(locacoes);
        }

        /// <summary>
        /// Lista locações finalizadas
        /// </summary>
        /// <returns>Lista de locações finalizadas</returns>
        [HttpGet("finalizadas")]
        public async Task<ActionResult<IEnumerable<LocacaoDTO>>> GetFinalizadas()
        {
            var locacoes = await _locacaoService.GetFinalizadasAsync();
            return Ok(locacoes);
        }

        /// <summary>
        /// Cria uma nova locação
        /// </summary>
        /// <param name="createLocacaoDTO">Dados da locação</param>
        /// <returns>Locação criada</returns>
        [HttpPost]
        public async Task<ActionResult<LocacaoDTO>> Create([FromBody] CreateLocacaoDTO createLocacaoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var locacao = await _locacaoService.CreateAsync(createLocacaoDTO);
                return CreatedAtAction(nameof(GetById), new { id = locacao.Id }, locacao);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza uma locação existente
        /// </summary>
        /// <param name="id">ID da locação</param>
        /// <param name="updateLocacaoDTO">Dados atualizados</param>
        /// <returns>Locação atualizada</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<LocacaoDTO>> Update(int id, [FromBody] UpdateLocacaoDTO updateLocacaoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var locacao = await _locacaoService.UpdateAsync(id, updateLocacaoDTO);
                return Ok(locacao);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma locação
        /// </summary>
        /// <param name="id">ID da locação</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _locacaoService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Inicia uma locação
        /// </summary>
        /// <param name="id">ID da locação</param>
        /// <returns>Locação iniciada</returns>
        [HttpPatch("{id}/iniciar")]
        public async Task<ActionResult<LocacaoDTO>> Iniciar(int id)
        {
            try
            {
                var locacao = await _locacaoService.IniciarAsync(id);
                return Ok(locacao);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Finaliza uma locação
        /// </summary>
        /// <param name="id">ID da locação</param>
        /// <returns>Locação finalizada</returns>
        [HttpPatch("{id}/finalizar")]
        public async Task<ActionResult<LocacaoDTO>> Finalizar(int id)
        {
            try
            {
                var locacao = await _locacaoService.FinalizarAsync(id);
                return Ok(locacao);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cancela uma locação
        /// </summary>
        /// <param name="id">ID da locação</param>
        /// <returns>Locação cancelada</returns>
        [HttpPatch("{id}/cancelar")]
        public async Task<ActionResult<LocacaoDTO>> Cancelar(int id)
        {
            try
            {
                var locacao = await _locacaoService.CancelarAsync(id);
                return Ok(locacao);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Calcula o valor total de uma locação
        /// </summary>
        /// <param name="id">ID da locação</param>
        /// <returns>Valor total calculado</returns>
        [HttpGet("{id}/calcular-valor")]
        public async Task<ActionResult<decimal>> CalcularValorTotal(int id)
        {
            try
            {
                var valorTotal = await _locacaoService.CalcularValorTotalAsync(id);
                return Ok(new { valorTotal });
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
