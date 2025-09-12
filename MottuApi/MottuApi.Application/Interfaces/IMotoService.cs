using System.Collections.Generic;
using System.Threading.Tasks;
using MottuApi.Application.DTOs;

namespace MottuApi.Application.Interfaces
{
    public interface IMotoService
    {
        Task<MotoDTO> GetByIdAsync(int id);
        Task<MotoDTO> GetByPlacaAsync(string placa);
        Task<IEnumerable<MotoDTO>> GetAllAsync();
        Task<PagedResultDTO<MotoDTO>> GetAllPagedAsync(int page, int pageSize);
        Task<IEnumerable<MotoDTO>> GetByFilialIdAsync(int filialId);
        Task<MotoDTO> CreateAsync(CreateMotoDTO createMotoDTO);
        Task<MotoDTO> UpdateAsync(int id, UpdateMotoDTO updateMotoDTO);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<MotoDTO> MarcarComoDisponivelAsync(int id);
        Task<MotoDTO> MarcarComoIndisponivelAsync(int id);
    }
} 