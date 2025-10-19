using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace MottuApi.Infrastructure.HealthChecks
{
    public class MongoHealthCheck : IHealthCheck
    {
        private readonly IMongoClient _mongoClient;

        public MongoHealthCheck(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                // Ping no MongoDB para verificar conectividade
                await _mongoClient.ListDatabaseNamesAsync(cancellationToken);
                
                return HealthCheckResult.Healthy("MongoDB está conectado e funcionando");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("MongoDB não está acessível", ex);
            }
        }
    }
}

