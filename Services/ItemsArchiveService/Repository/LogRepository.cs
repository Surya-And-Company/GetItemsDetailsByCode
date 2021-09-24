using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemsArchiveService.Data;
using ItemsArchiveService.Model;
using MongoDB.Driver;

namespace ItemsArchiveService.Repository
{
    public class LogRepository : ILogRepository
    {
         private readonly IDbContext _context;
        public LogRepository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateLog(Log log)
        {
            await _context.Logs.InsertOneAsync(log);
        }


        public async Task<IEnumerable<Log>> GetLogs(DateTime logdate, int top = 1)
        {
            var endDate = logdate.AddDays(1);
            var builder = Builders<Log>.Filter;
            var filter = builder.Gt(x => x.LogDate, logdate) & builder.Lt(x => x.LogDate, endDate);
            return await _context.Logs.Find(filter).SortByDescending(x => x.LogDate).Limit(top).ToListAsync();
        }
    }
}