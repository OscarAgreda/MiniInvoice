using System;
namespace DDDCleanArchStarter.Infrastructure.Services
{
    public interface IAppLoggerService<T>
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(string message, params object[] args);
        void LogError(Exception ex, string v);
    }
}