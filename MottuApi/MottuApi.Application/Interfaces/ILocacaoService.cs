using MottuApi.Application.DTOs;

namespace MottuApi.Application.Interfaces
{
    public interface ILocacaoService
    {
        Task<PagedResult<LocacaoDTO>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<LocacaoDTO?> GetByIdAsync(int id);
        Task<IEnumerable<LocacaoDTO>> GetByMotoIdAsync(int motoId);
        Task<IEnumerable<LocacaoDTO>> GetByFilialIdAsync(int filialId);
        Task<IEnumerable<LocacaoDTO>> GetByClienteCpfAsync(string cpf);
        Task<IEnumerable<LocacaoDTO>> GetByPeriodoAsync(DateTime inicio, DateTime fim);
        Task<IEnumerable<LocacaoDTO>> GetAtivasAsync();
        Task<IEnumerable<LocacaoDTO>> GetFinalizadasAsync();
        Task<LocacaoDTO> CreateAsync(CreateLocacaoDTO createLocacaoDTO);
        Task<LocacaoDTO> UpdateAsync(int id, UpdateLocacaoDTO updateLocacaoDTO);
        Task DeleteAsync(int id);
        Task<LocacaoDTO> IniciarAsync(int id);
        Task<LocacaoDTO> FinalizarAsync(int id);
        Task<LocacaoDTO> CancelarAsync(int id);
        Task<decimal> CalcularValorTotalAsync(int id);
        Task<bool> MotoEstaDisponivelAsync(int motoId, DateTime dataInicio, DateTime? dataFim, int? excludeLocacaoId = null);
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
