using ItemsArchiveService.Model;
using MongoDB.Driver;

namespace ItemsArchiveService.Data
{
    public interface IDbContext
    {
        IMongoCollection<User> Users { get; }
        IMongoCollection<Item> Items { get; }
        IMongoCollection<Log> Logs { get; }
        IMongoCollection<ThirdPartyAllowed> ThirdPartiesAllowed { get;}

    }
}