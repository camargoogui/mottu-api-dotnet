using System.Collections.Generic;
using System.Threading.Tasks;
using MottuApi.Application.DTOs;

namespace MottuApi.Application.Interfaces
{
    public interface IFilialService
    {
        Task<FilialDTO> GetByIdAsync(int id);
        Task<IEnumerable<FilialDTO>> GetAllAsync();
        Task<PagedResultDTO<FilialDTO>> GetAllPagedAsync(int page, int pageSize);
        Task<FilialDTO> CreateAsync(CreateFilialDTO createFilialDTO);
        Task<FilialDTO> UpdateAsync(int id, UpdateFilialDTO updateFilialDTO);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<FilialDTO> AtivarAsync(int id);
        Task<FilialDTO> DesativarAsync(int id);
    }
} 