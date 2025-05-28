using MongoDB.Driver;
using Microsoft.Extensions.Options;
using csharp_demo_api.Domain.Entities;
using csharp_demo_api.Infrastructure.Persistence.Settings;

namespace csharp_demo_api.Infrastructure.Persistence
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSetting> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _database = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<TaskEntity> Tasks => _database.GetCollection<TaskEntity>("task");
    }
}
