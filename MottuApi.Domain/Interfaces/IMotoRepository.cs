using System.Collections.Generic;
using System.Threading.Tasks;
using MottuApi.Domain.Entities;

namespace MottuApi.Domain.Interfaces
{
    public interface IMotoRepository
    {
        Task<Moto> GetByIdAsync(int id);
        Task<Moto> GetByPlacaAsync(string placa);
        Task<IEnumerable<Moto>> GetAllAsync();
        Task<IEnumerable<Moto>> GetByFilialIdAsync(int filialId);
        Task<Moto> AddAsync(Moto moto);
        Task UpdateAsync(Moto moto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByPlacaAsync(string placa);
    }
} 