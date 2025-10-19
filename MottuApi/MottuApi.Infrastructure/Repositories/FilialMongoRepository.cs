using MongoDB.Driver;
using MottuApi.Domain.Entities;
using MottuApi.Domain.Interfaces;
using MottuApi.Infrastructure.Data;

namespace MottuApi.Infrastructure.Repositories
{
    public class FilialMongoRepository : IFilialRepository
    {
        private readonly MongoDbContext _context;

        public FilialMongoRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Filial?> GetByIdAsync(int id)
        {
            return await _context.Filiais.Find(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Filial>> GetAllAsync()
        {
            return await _context.Filiais.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Filial>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Filiais.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return (int)await _context.Filiais.CountDocumentsAsync(_ => true);
        }

        public async Task<Filial> AddAsync(Filial filial)
        {
            // Gerar ID único se não foi definido
            if (filial.Id == 0)
            {
                var maxId = await _context.Filiais.Find(_ => true)
                    .Sort(Builders<Filial>.Sort.Descending(f => f.Id))
                    .Limit(1)
                    .FirstOrDefaultAsync();
                
                filial.DefinirId((maxId?.Id ?? 0) + 1);
            }
            
            await _context.Filiais.InsertOneAsync(filial);
            return filial;
        }

        public async Task UpdateAsync(Filial filial)
        {
            await _context.Filiais.ReplaceOneAsync(f => f.Id == filial.Id, filial);
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Filiais.DeleteOneAsync(f => f.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Filiais.CountDocumentsAsync(f => f.Id == id) > 0;
        }

        public async Task<bool> ExistsByNomeAsync(string nome)
        {
            return await _context.Filiais.CountDocumentsAsync(f => f.Nome == nome) > 0;
        }
    }
}

