using MottuApi.Domain.Entities;

namespace MottuApi.Domain.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<IEnumerable<Locacao>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<Locacao?> GetByIdAsync(int id);
        Task<IEnumerable<Locacao>> GetByMotoIdAsync(int motoId);
        Task<IEnumerable<Locacao>> GetByFilialIdAsync(int filialId);
        Task<IEnumerable<Locacao>> GetByClienteCpfAsync(string cpf);
        Task<IEnumerable<Locacao>> GetByPeriodoAsync(DateTime inicio, DateTime fim);
        Task<IEnumerable<Locacao>> GetAtivasAsync();
        Task<IEnumerable<Locacao>> GetFinalizadasAsync();
        Task<Locacao> CreateAsync(Locacao locacao);
        Task<Locacao> UpdateAsync(Locacao locacao);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> GetTotalCountAsync();
        Task<bool> MotoEstaDisponivelAsync(int motoId, DateTime dataInicio, DateTime? dataFim, int? excludeLocacaoId = null);
    }
}
