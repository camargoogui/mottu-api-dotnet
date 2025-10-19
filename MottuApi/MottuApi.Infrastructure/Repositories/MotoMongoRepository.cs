using MongoDB.Driver;
using MottuApi.Domain.Entities;
using MottuApi.Domain.Interfaces;
using MottuApi.Infrastructure.Data;

namespace MottuApi.Infrastructure.Repositories
{
    public class MotoMongoRepository : IMotoRepository
    {
        private readonly MongoDbContext _context;

        public MotoMongoRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Moto?> GetByIdAsync(int id)
        {
            return await _context.Motos.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Moto?> GetByPlacaAsync(string placa)
        {
            return await _context.Motos.Find(m => m.Placa == placa.ToUpper()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Moto>> GetAllAsync()
        {
            return await _context.Motos.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Moto>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Motos.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return (int)await _context.Motos.CountDocumentsAsync(_ => true);
        }

        public async Task<IEnumerable<Moto>> GetByFilialIdAsync(int filialId)
        {
            return await _context.Motos.Find(m => m.FilialId == filialId).ToListAsync();
        }

        public async Task<Moto> AddAsync(Moto moto)
        {
            // Gerar ID único se não foi definido
            if (moto.Id == 0)
            {
                var maxId = await _context.Motos.Find(_ => true)
                    .Sort(Builders<Moto>.Sort.Descending(m => m.Id))
                    .Limit(1)
                    .FirstOrDefaultAsync();
                
                moto.DefinirId((maxId?.Id ?? 0) + 1);
            }
            
            await _context.Motos.InsertOneAsync(moto);
            return moto;
        }

        public async Task UpdateAsync(Moto moto)
        {
            await _context.Motos.ReplaceOneAsync(m => m.Id == moto.Id, moto);
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Motos.DeleteOneAsync(m => m.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Motos.CountDocumentsAsync(m => m.Id == id) > 0;
        }

        public async Task<bool> ExistsByPlacaAsync(string placa)
        {
            return await _context.Motos.CountDocumentsAsync(m => m.Placa == placa.ToUpper()) > 0;
        }
    }
}

