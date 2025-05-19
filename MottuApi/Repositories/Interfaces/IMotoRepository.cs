using MottuApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MottuApi.Repositories.Interfaces
{
    public interface IMotoRepository
    {
        Task<IEnumerable<Moto>> GetAllAsync();
        Task<Moto> GetByIdAsync(int id);
        Task<Moto> AddAsync(Moto moto);
        Task<bool> UpdateAsync(Moto moto);
        Task<bool> DeleteAsync(int id);
    }
}
