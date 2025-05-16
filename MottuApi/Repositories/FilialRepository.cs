using MottuApi.Data;
using MottuApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MottuApi.Repositories.Interfaces;

namespace MottuApi.Repositories
{
    public class FilialRepository : IFilialRepository
    {
        private readonly AppDbContext _context;
        public FilialRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Filial>> GetAllAsync()
        {
            return await _context.Filiais.ToListAsync();
        }

        public async Task<Filial> GetByIdAsync(int id)
        {
            return await _context.Filiais.FindAsync(id);
        }

        public async Task<Filial> AddAsync(Filial filial)
        {
            _context.Filiais.Add(filial);
            await _context.SaveChangesAsync();
            return filial;
        }

        public async Task<bool> UpdateAsync(Filial filial)
        {
            _context.Filiais.Update(filial);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var filial = await _context.Filiais.FindAsync(id);
            if (filial == null) return false;
            _context.Filiais.Remove(filial);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
