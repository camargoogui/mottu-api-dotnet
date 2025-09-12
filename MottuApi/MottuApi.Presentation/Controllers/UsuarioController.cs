using Microsoft.AspNetCore.Mvc;
using MottuApi.Application.DTOs;
using MottuApi.Application.Interfaces;

namespace MottuApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Lista todos os usuários com paginação
        /// </summary>
        /// <param name="page">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10)</param>
        /// <returns>Lista paginada de usuários</returns>
        [HttpGet]
        public async Task<ActionResult<PagedResult<UsuarioDTO>>> GetAll(int page = 1, int pageSize = 10)
        {
            var usuarios = await _usuarioService.GetAllAsync(page, pageSize);
            return Ok(usuarios);
        }

        /// <summary>
        /// Busca um usuário por ID
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Dados do usuário</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        /// <summary>
        /// Busca um usuário por email
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <returns>Dados do usuário</returns>
        [HttpGet("por-email")]
        public async Task<ActionResult<UsuarioDTO>> GetByEmail([FromQuery] string email)
        {
            var usuario = await _usuarioService.GetByEmailAsync(email);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        /// <summary>
        /// Busca um usuário por CPF
        /// </summary>
        /// <param name="cpf">CPF do usuário</param>
        /// <returns>Dados do usuário</returns>
        [HttpGet("por-cpf")]
        public async Task<ActionResult<UsuarioDTO>> GetByCpf([FromQuery] string cpf)
        {
            var usuario = await _usuarioService.GetByCpfAsync(cpf);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        /// <summary>
        /// Lista usuários de uma filial específica
        /// </summary>
        /// <param name="filialId">ID da filial</param>
        /// <returns>Lista de usuários da filial</returns>
        [HttpGet("por-filial/{filialId}")]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetByFilialId(int filialId)
        {
            var usuarios = await _usuarioService.GetByFilialIdAsync(filialId);
            return Ok(usuarios);
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="createUsuarioDTO">Dados do usuário</param>
        /// <returns>Usuário criado</returns>
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Create([FromBody] CreateUsuarioDTO createUsuarioDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var usuario = await _usuarioService.CreateAsync(createUsuarioDTO);
                return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="updateUsuarioDTO">Dados atualizados</param>
        /// <returns>Usuário atualizado</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDTO>> Update(int id, [FromBody] UpdateUsuarioDTO updateUsuarioDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var usuario = await _usuarioService.UpdateAsync(id, updateUsuarioDTO);
                return Ok(usuario);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um usuário
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _usuarioService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Ativa um usuário
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Usuário ativado</returns>
        [HttpPatch("{id}/ativar")]
        public async Task<ActionResult<UsuarioDTO>> Ativar(int id)
        {
            try
            {
                var usuario = await _usuarioService.AtivarAsync(id);
                return Ok(usuario);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Desativa um usuário
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Usuário desativado</returns>
        [HttpPatch("{id}/desativar")]
        public async Task<ActionResult<UsuarioDTO>> Desativar(int id)
        {
            try
            {
                var usuario = await _usuarioService.DesativarAsync(id);
                return Ok(usuario);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
