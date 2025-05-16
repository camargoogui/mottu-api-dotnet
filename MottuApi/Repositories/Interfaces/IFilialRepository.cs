using MottuApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuApi.Repositories.Interfaces
{
    public interface IFilialRepository
    {
        Task<IEnumerable<Filial>> GetAllAsync();
        Task<Filial> GetByIdAsync(int id);
        Task<Filial> AddAsync(Filial filial);
        Task<bool> UpdateAsync(Filial filial);
        Task<bool> DeleteAsync(int id);
    }
}
