using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MottuApi.Domain.Entities;
using MottuApi.Domain.Interfaces;
using MottuApi.Infrastructure.Data;

namespace MottuApi.Infrastructure.Repositories
{
    public class FilialRepository : IFilialRepository
    {
        private readonly ApplicationDbContext _context;

        public FilialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Filial> GetByIdAsync(int id)
        {
            return await _context.Filiais
                .Include(f => f.Motos)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Filial>> GetAllAsync()
        {
            return await _context.Filiais
                .Include(f => f.Motos)
                .ToListAsync();
        }

        public async Task<IEnumerable<Filial>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Filiais
                .Include(f => f.Motos)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Filiais.CountAsync();
        }

        public async Task<Filial> AddAsync(Filial filial)
        {
            await _context.Filiais.AddAsync(filial);
            await _context.SaveChangesAsync();
            return filial;
        }

        public async Task UpdateAsync(Filial filial)
        {
            _context.Filiais.Update(filial);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var filial = await GetByIdAsync(id);
            if (filial != null)
            {
                _context.Filiais.Remove(filial);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Filiais.AnyAsync(f => f.Id == id);
        }

        public async Task<bool> ExistsByNomeAsync(string nome)
        {
            return await _context.Filiais.AnyAsync(f => f.Nome == nome);
        }
    }
} 