using System;
using ItemsArchiveService.Model;
using ItemsArchiveService.Repository;
using Microsoft.AspNetCore.Diagnostics;

namespace ItemsArchiveService.ExceptionHandler
{
    public class LogNLog : ILog
    {

        private readonly ILogRepository _logRepository;
        public LogNLog(ILogRepository logRepository)
        {
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
        }

        public void Debug(string message)
        {
            _logRepository.CreateLog(new Log() { ExceptionMsg = message, ExceptionType = "debug", LogDate = DateTime.Now });
        }

        public void Error(IExceptionHandlerPathFeature exception)
        {
            var log = new Log();
            log.ExceptionMsg = exception.Error.Message;
            log.ExceptionType = "Error";
            log.ExceptionURL = exception.Path;
            log.StackTrace = exception.Error.StackTrace;
            log.LogDate = DateTime.Now;
            _logRepository.CreateLog(log);
        }

        public void Information(string message)
        {
            _logRepository.CreateLog(new Log() { ExceptionMsg = message, ExceptionType = "Info", LogDate = DateTime.Now });
        }

        public void Warning(string message)
        {
            _logRepository.CreateLog(new Log() { ExceptionMsg = message, ExceptionType = "Warning", LogDate = DateTime.Now });
        }
    }
}