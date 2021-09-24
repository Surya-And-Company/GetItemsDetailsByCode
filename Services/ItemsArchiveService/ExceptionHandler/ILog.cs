using Microsoft.AspNetCore.Diagnostics;

namespace ItemsArchiveService.ExceptionHandler
{
    public interface ILog
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(IExceptionHandlerPathFeature exception);
    }
}