using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemsArchiveService.Model;

namespace ItemsArchiveService.Repository
{
    public interface ILogRepository
    {
        Task CreateLog(Log log);
        Task<IEnumerable<Log>> GetLogs(DateTime logdate, int top = 1);
    }
}