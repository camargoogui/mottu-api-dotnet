using MongoDB.Driver;
using MottuApi.Domain.Entities;
using MottuApi.Domain.Interfaces;
using MottuApi.Infrastructure.Data;

namespace MottuApi.Infrastructure.Repositories
{
    public class LocacaoMongoRepository : ILocacaoRepository
    {
        private readonly MongoDbContext _context;

        public LocacaoMongoRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Locacao>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            return await _context.Locacoes.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<Locacao?> GetByIdAsync(int id)
        {
            return await _context.Locacoes.Find(l => l.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Locacao>> GetByMotoIdAsync(int motoId)
        {
            return await _context.Locacoes.Find(l => l.MotoId == motoId).ToListAsync();
        }

        public async Task<IEnumerable<Locacao>> GetByFilialIdAsync(int filialId)
        {
            return await _context.Locacoes.Find(l => l.FilialId == filialId).ToListAsync();
        }

        public async Task<IEnumerable<Locacao>> GetByClienteCpfAsync(string cpf)
        {
            return await _context.Locacoes.Find(l => l.ClienteCpf == cpf).ToListAsync();
        }

        public async Task<IEnumerable<Locacao>> GetByPeriodoAsync(DateTime inicio, DateTime fim)
        {
            return await _context.Locacoes.Find(l => l.DataInicio >= inicio && l.DataInicio <= fim).ToListAsync();
        }

        public async Task<IEnumerable<Locacao>> GetAtivasAsync()
        {
            return await _context.Locacoes.Find(l => l.Status == StatusLocacao.Iniciada).ToListAsync();
        }

        public async Task<IEnumerable<Locacao>> GetFinalizadasAsync()
        {
            return await _context.Locacoes.Find(l => l.Status == StatusLocacao.Finalizada).ToListAsync();
        }

        public async Task<Locacao> CreateAsync(Locacao locacao)
        {
            // Gerar ID único se não foi definido
            if (locacao.Id == 0)
            {
                var maxId = await _context.Locacoes.Find(_ => true)
                    .Sort(Builders<Locacao>.Sort.Descending(l => l.Id))
                    .Limit(1)
                    .FirstOrDefaultAsync();
                
                locacao.DefinirId((maxId?.Id ?? 0) + 1);
            }
            
            await _context.Locacoes.InsertOneAsync(locacao);
            return locacao;
        }

        public async Task<Locacao> UpdateAsync(Locacao locacao)
        {
            await _context.Locacoes.ReplaceOneAsync(l => l.Id == locacao.Id, locacao);
            return locacao;
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Locacoes.DeleteOneAsync(l => l.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Locacoes.CountDocumentsAsync(l => l.Id == id) > 0;
        }

        public async Task<int> GetTotalCountAsync()
        {
            return (int)await _context.Locacoes.CountDocumentsAsync(_ => true);
        }

        public async Task<bool> MotoEstaDisponivelAsync(int motoId, DateTime dataInicio, DateTime? dataFim, int? excludeLocacaoId = null)
        {
            var dataFimCheck = dataFim ?? dataInicio.AddHours(1);
            
            var filter = Builders<Locacao>.Filter.And(
                Builders<Locacao>.Filter.Eq(l => l.MotoId, motoId),
                Builders<Locacao>.Filter.Ne(l => l.Status, StatusLocacao.Cancelada),
                Builders<Locacao>.Filter.Or(
                    Builders<Locacao>.Filter.And(
                        Builders<Locacao>.Filter.Lte(l => l.DataInicio, dataInicio),
                        Builders<Locacao>.Filter.Or(
                            Builders<Locacao>.Filter.Eq(l => l.DataFim, null),
                            Builders<Locacao>.Filter.Gt(l => l.DataFim, dataInicio)
                        )
                    ),
                    Builders<Locacao>.Filter.And(
                        Builders<Locacao>.Filter.Lt(l => l.DataInicio, dataFimCheck),
                        Builders<Locacao>.Filter.Or(
                            Builders<Locacao>.Filter.Eq(l => l.DataFim, null),
                            Builders<Locacao>.Filter.Gte(l => l.DataFim, dataFimCheck)
                        )
                    ),
                    Builders<Locacao>.Filter.And(
                        Builders<Locacao>.Filter.Gte(l => l.DataInicio, dataInicio),
                        Builders<Locacao>.Filter.Lt(l => l.DataInicio, dataFimCheck)
                    )
                )
            );

            if (excludeLocacaoId.HasValue)
            {
                filter = Builders<Locacao>.Filter.And(
                    filter,
                    Builders<Locacao>.Filter.Ne(l => l.Id, excludeLocacaoId.Value)
                );
            }

            var conflitantes = await _context.Locacoes.CountDocumentsAsync(filter);
            return conflitantes == 0;
        }
    }
}

