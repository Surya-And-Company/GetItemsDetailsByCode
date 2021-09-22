using ItemsArchiveService.Model;
using MongoDB.Driver;

namespace ItemsArchiveService.Data
{
    public class DbContext : IDbContext
    {
        public IMongoCollection<User> Users { get; }

        public IMongoCollection<Item> Items { get; }

        public IMongoCollection<Log> Logs { get; }

        public IMongoCollection<ThirdPartyAllowed> ThirdPartiesAllowed { get; }
    }
}