using MottuApi.Domain.Entities;

namespace MottuApi.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario?> GetByEmailAsync(string email);
        Task<Usuario?> GetByCpfAsync(string cpf);
        Task<IEnumerable<Usuario>> GetByFilialIdAsync(int filialId);
        Task<Usuario> CreateAsync(Usuario usuario);
        Task<Usuario> UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> GetTotalCountAsync();
    }
}
