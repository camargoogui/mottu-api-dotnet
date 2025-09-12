using MottuApi.Application.DTOs;

namespace MottuApi.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<PagedResult<UsuarioDTO>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<UsuarioDTO?> GetByIdAsync(int id);
        Task<UsuarioDTO?> GetByEmailAsync(string email);
        Task<UsuarioDTO?> GetByCpfAsync(string cpf);
        Task<IEnumerable<UsuarioDTO>> GetByFilialIdAsync(int filialId);
        Task<UsuarioDTO> CreateAsync(CreateUsuarioDTO createUsuarioDTO);
        Task<UsuarioDTO> UpdateAsync(int id, UpdateUsuarioDTO updateUsuarioDTO);
        Task DeleteAsync(int id);
        Task<UsuarioDTO> AtivarAsync(int id);
        Task<UsuarioDTO> DesativarAsync(int id);
    }

    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => Page < TotalPages;
        public bool HasPreviousPage => Page > 1;
    }
}
