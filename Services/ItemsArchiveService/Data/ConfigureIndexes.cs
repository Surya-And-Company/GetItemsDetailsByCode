using System;
using System.Threading;
using System.Threading.Tasks;
using ItemsArchiveService.Model;
using MongoDB.Driver;

namespace ItemsArchiveService.Data
{
    public class ConfigureIndexes
    {
        private readonly IDbContext _context;
         public ConfigureIndexes(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {         
            var IndexOnCode = Builders<Item>.IndexKeys.Ascending(x => x.Code);
            await _context.Items.Indexes.CreateOneAsync(new CreateIndexModel<Item>(IndexOnCode), cancellationToken: cancellationToken);   
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}