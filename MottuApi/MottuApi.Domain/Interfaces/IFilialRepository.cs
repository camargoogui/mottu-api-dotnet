using System.Collections.Generic;
using System.Threading.Tasks;
using MottuApi.Domain.Entities;

namespace MottuApi.Domain.Interfaces
{
    public interface IFilialRepository
    {
        Task<Filial?> GetByIdAsync(int id);
        Task<IEnumerable<Filial>> GetAllAsync();
        Task<IEnumerable<Filial>> GetPagedAsync(int page, int pageSize);
        Task<int> GetCountAsync();
        Task<Filial> AddAsync(Filial filial);
        Task UpdateAsync(Filial filial);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByNomeAsync(string nome);
    }
} 