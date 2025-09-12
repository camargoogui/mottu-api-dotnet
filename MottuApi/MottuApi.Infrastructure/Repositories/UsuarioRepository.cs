using Microsoft.EntityFrameworkCore;
using MottuApi.Domain.Entities;
using MottuApi.Domain.Interfaces;
using MottuApi.Infrastructure.Data;

namespace MottuApi.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            return await _context.Usuarios
                .Include(u => u.Filial)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Filial)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios
                .Include(u => u.Filial)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> GetByCpfAsync(string cpf)
        {
            return await _context.Usuarios
                .Include(u => u.Filial)
                .FirstOrDefaultAsync(u => u.Cpf == cpf);
        }

        public async Task<IEnumerable<Usuario>> GetByFilialIdAsync(int filialId)
        {
            return await _context.Usuarios
                .Include(u => u.Filial)
                .Where(u => u.FilialId == filialId)
                .ToListAsync();
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Usuarios.AnyAsync(u => u.Id == id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Usuarios.CountAsync();
        }
    }
}
