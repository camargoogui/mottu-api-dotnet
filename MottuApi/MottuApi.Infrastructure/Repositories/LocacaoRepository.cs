using Microsoft.EntityFrameworkCore;
using MottuApi.Domain.Entities;
using MottuApi.Domain.Interfaces;
using MottuApi.Infrastructure.Data;

namespace MottuApi.Infrastructure.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public LocacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Locacao>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            return await _context.Locacoes
                .Include(l => l.Moto)
                .Include(l => l.Filial)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Locacao?> GetByIdAsync(int id)
        {
            return await _context.Locacoes
                .Include(l => l.Moto)
                .Include(l => l.Filial)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Locacao>> GetByMotoIdAsync(int motoId)
        {
            return await _context.Locacoes
                .Include(l => l.Moto)
                .Include(l => l.Filial)
                .Where(l => l.MotoId == motoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Locacao>> GetByFilialIdAsync(int filialId)
        {
            return await _context.Locacoes
                .Include(l => l.Moto)
                .Include(l => l.Filial)
                .Where(l => l.FilialId == filialId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Locacao>> GetByClienteCpfAsync(string cpf)
        {
            return await _context.Locacoes
                .Include(l => l.Moto)
                .Include(l => l.Filial)
                .Where(l => l.ClienteCpf == cpf)
                .ToListAsync();
        }

        public async Task<IEnumerable<Locacao>> GetByPeriodoAsync(DateTime inicio, DateTime fim)
        {
            return await _context.Locacoes
                .Include(l => l.Moto)
                .Include(l => l.Filial)
                .Where(l => l.DataInicio >= inicio && l.DataInicio <= fim)
                .ToListAsync();
        }

        public async Task<IEnumerable<Locacao>> GetAtivasAsync()
        {
            return await _context.Locacoes
                .Include(l => l.Moto)
                .Include(l => l.Filial)
                .Where(l => l.Status == StatusLocacao.Iniciada)
                .ToListAsync();
        }

        public async Task<IEnumerable<Locacao>> GetFinalizadasAsync()
        {
            return await _context.Locacoes
                .Include(l => l.Moto)
                .Include(l => l.Filial)
                .Where(l => l.Status == StatusLocacao.Finalizada)
                .ToListAsync();
        }

        public async Task<Locacao> CreateAsync(Locacao locacao)
        {
            _context.Locacoes.Add(locacao);
            await _context.SaveChangesAsync();
            return locacao;
        }

        public async Task<Locacao> UpdateAsync(Locacao locacao)
        {
            _context.Locacoes.Update(locacao);
            await _context.SaveChangesAsync();
            return locacao;
        }

        public async Task DeleteAsync(int id)
        {
            var locacao = await _context.Locacoes.FindAsync(id);
            if (locacao != null)
            {
                _context.Locacoes.Remove(locacao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Locacoes.AnyAsync(l => l.Id == id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Locacoes.CountAsync();
        }

        public async Task<bool> MotoEstaDisponivelAsync(int motoId, DateTime dataInicio, DateTime? dataFim, int? excludeLocacaoId = null)
        {
            var dataFimCheck = dataFim ?? dataInicio.AddHours(1); // Se não informado, assume 1 hora
            
            var locacoesConflitantes = await _context.Locacoes
                .Where(l => l.MotoId == motoId && 
                           l.Status != StatusLocacao.Cancelada &&
                           (excludeLocacaoId == null || l.Id != excludeLocacaoId) &&
                           ((l.DataInicio <= dataInicio && (l.DataFim == null || l.DataFim > dataInicio)) ||
                            (l.DataInicio < dataFimCheck && (l.DataFim == null || l.DataFim >= dataFimCheck)) ||
                            (l.DataInicio >= dataInicio && l.DataInicio < dataFimCheck)))
                .AnyAsync();

            return !locacoesConflitantes;
        }
    }
}
