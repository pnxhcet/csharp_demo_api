
namespace csharp_demo_api.Infrastructure.Persistence.Settings;
public class MongoDbSetting
{
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
}
