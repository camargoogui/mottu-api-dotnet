using MottuApi.Data;
using MottuApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MottuApi.Repositories.Interfaces;

namespace MottuApi.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly AppDbContext _context;
        public MotoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Moto>> GetAllAsync()
        {
            return await _context.Motos.Include(m => m.Filial).ToListAsync();
        }

        public async Task<Moto> GetByIdAsync(int id)
        {
            return await _context.Motos.Include(m => m.Filial).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Moto> AddAsync(Moto moto)
        {
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            return moto;
        }

        public async Task<bool> UpdateAsync(Moto moto)
        {
            _context.Motos.Update(moto);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return false;
            _context.Motos.Remove(moto);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
