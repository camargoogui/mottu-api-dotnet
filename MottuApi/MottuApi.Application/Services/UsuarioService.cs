using AutoMapper;
using MottuApi.Application.DTOs;
using MottuApi.Application.Interfaces;
using MottuApi.Domain.Entities;
using MottuApi.Domain.Interfaces;

namespace MottuApi.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<UsuarioDTO>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            var usuarios = await _usuarioRepository.GetAllAsync(page, pageSize);
            var totalCount = await _usuarioRepository.GetTotalCountAsync();
            
            var usuarioDTOs = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            
            // Adicionar HATEOAS
            foreach (var usuario in usuarioDTOs)
            {
                usuario.Links = GenerateLinks(usuario.Id);
            }

            return new PagedResult<UsuarioDTO>
            {
                Data = usuarioDTOs,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<UsuarioDTO?> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return null;

            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            usuarioDTO.Links = GenerateLinks(usuarioDTO.Id);
            return usuarioDTO;
        }

        public async Task<UsuarioDTO?> GetByEmailAsync(string email)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            if (usuario == null) return null;

            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            usuarioDTO.Links = GenerateLinks(usuarioDTO.Id);
            return usuarioDTO;
        }

        public async Task<UsuarioDTO?> GetByCpfAsync(string cpf)
        {
            var usuario = await _usuarioRepository.GetByCpfAsync(cpf);
            if (usuario == null) return null;

            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            usuarioDTO.Links = GenerateLinks(usuarioDTO.Id);
            return usuarioDTO;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetByFilialIdAsync(int filialId)
        {
            var usuarios = await _usuarioRepository.GetByFilialIdAsync(filialId);
            var usuarioDTOs = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            
            foreach (var usuario in usuarioDTOs)
            {
                usuario.Links = GenerateLinks(usuario.Id);
            }

            return usuarioDTOs;
        }

        public async Task<UsuarioDTO> CreateAsync(CreateUsuarioDTO createUsuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(createUsuarioDTO);
            var createdUsuario = await _usuarioRepository.CreateAsync(usuario);
            var usuarioDTO = _mapper.Map<UsuarioDTO>(createdUsuario);
            usuarioDTO.Links = GenerateLinks(usuarioDTO.Id);
            return usuarioDTO;
        }

        public async Task<UsuarioDTO> UpdateAsync(int id, UpdateUsuarioDTO updateUsuarioDTO)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
                throw new ArgumentException("Usuário não encontrado");

            _mapper.Map(updateUsuarioDTO, existingUsuario);
            var updatedUsuario = await _usuarioRepository.UpdateAsync(existingUsuario);
            var usuarioDTO = _mapper.Map<UsuarioDTO>(updatedUsuario);
            usuarioDTO.Links = GenerateLinks(usuarioDTO.Id);
            return usuarioDTO;
        }

        public async Task DeleteAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<UsuarioDTO> AtivarAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado");

            usuario.Ativar();
            var updatedUsuario = await _usuarioRepository.UpdateAsync(usuario);
            var usuarioDTO = _mapper.Map<UsuarioDTO>(updatedUsuario);
            usuarioDTO.Links = GenerateLinks(usuarioDTO.Id);
            return usuarioDTO;
        }

        public async Task<UsuarioDTO> DesativarAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado");

            usuario.Desativar();
            var updatedUsuario = await _usuarioRepository.UpdateAsync(usuario);
            var usuarioDTO = _mapper.Map<UsuarioDTO>(updatedUsuario);
            usuarioDTO.Links = GenerateLinks(usuarioDTO.Id);
            return usuarioDTO;
        }

        private List<LinkDTO> GenerateLinks(int id)
        {
            return new List<LinkDTO>
            {
                new() { Href = $"/api/usuario/{id}", Rel = "self", Method = "GET" },
                new() { Href = $"/api/usuario/{id}", Rel = "update", Method = "PUT" },
                new() { Href = $"/api/usuario/{id}", Rel = "delete", Method = "DELETE" },
                new() { Href = $"/api/usuario/{id}/ativar", Rel = "ativar", Method = "PATCH" },
                new() { Href = $"/api/usuario/{id}/desativar", Rel = "desativar", Method = "PATCH" }
            };
        }
    }
}
