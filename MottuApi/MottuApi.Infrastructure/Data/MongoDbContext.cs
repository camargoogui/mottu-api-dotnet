using MongoDB.Driver;
using MottuApi.Domain.Entities;

namespace MottuApi.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient mongoClient, string databaseName)
        {
            _database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<Filial> Filiais => _database.GetCollection<Filial>("filiais");
        public IMongoCollection<Moto> Motos => _database.GetCollection<Moto>("motos");
        public IMongoCollection<Locacao> Locacoes => _database.GetCollection<Locacao>("locacoes");
    }
}

