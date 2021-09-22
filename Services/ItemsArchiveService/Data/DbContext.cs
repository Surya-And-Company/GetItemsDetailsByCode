using ItemsArchiveService.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ItemsArchiveService.Data
{
    public class DbContext : IDbContext
    {

        public DbContext(IConfiguration configuration)
        { 
             var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
             var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
             Users = database.GetCollection<User>(configuration.GetValue<string>("DatabaseSettings:UserCollectionName"));
             Items = database.GetCollection<Item>(configuration.GetValue<string>("DatabaseSettings:ItemCollectionName"));
             Logs = database.GetCollection<Log>(configuration.GetValue<string>("DatabaseSettings:LogCollectionName"));
             ThirdPartiesAllowed = database.GetCollection<ThirdPartyAllowed>(configuration.GetValue<string>("DatabaseSettings:ThirdPartyAllowedCollectionName"));
        }
        public IMongoCollection<User> Users { get; }

        public IMongoCollection<Item> Items { get; }

        public IMongoCollection<Log> Logs { get; }

        public IMongoCollection<ThirdPartyAllowed> ThirdPartiesAllowed { get; }
    }
}