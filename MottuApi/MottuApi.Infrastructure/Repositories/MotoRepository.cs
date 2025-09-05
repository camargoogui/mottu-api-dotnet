using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MottuApi.Domain.Entities;
using MottuApi.Domain.Interfaces;
using MottuApi.Infrastructure.Data;

namespace MottuApi.Infrastructure.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly ApplicationDbContext _context;

        public MotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Moto> GetByIdAsync(int id)
        {
            return await _context.Motos
                .Include(m => m.Filial)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Moto> GetByPlacaAsync(string placa)
        {
            return await _context.Motos
                .Include(m => m.Filial)
                .FirstOrDefaultAsync(m => m.Placa == placa.ToUpper());
        }

        public async Task<IEnumerable<Moto>> GetAllAsync()
        {
            return await _context.Motos
                .Include(m => m.Filial)
                .ToListAsync();
        }

        public async Task<IEnumerable<Moto>> GetByFilialIdAsync(int filialId)
        {
            return await _context.Motos
                .Include(m => m.Filial)
                .Where(m => m.FilialId == filialId)
                .ToListAsync();
        }

        public async Task<Moto> AddAsync(Moto moto)
        {
            await _context.Motos.AddAsync(moto);
            await _context.SaveChangesAsync();
            return moto;
        }

        public async Task UpdateAsync(Moto moto)
        {
            _context.Motos.Update(moto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var moto = await GetByIdAsync(id);
            if (moto != null)
            {
                _context.Motos.Remove(moto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Motos.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> ExistsByPlacaAsync(string placa)
        {
            return await _context.Motos.AnyAsync(m => m.Placa == placa.ToUpper());
        }
    }
} 