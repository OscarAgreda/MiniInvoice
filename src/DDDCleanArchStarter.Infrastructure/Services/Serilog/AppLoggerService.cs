using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaamApp.Infrastructure.Data;
using Serilog;
using Serilog.Formatting.Json;
using ILogger = Serilog.ILogger;
namespace DDDCleanArchStarter.Infrastructure.Services
{
    public class AppLoggerService<T> : IAppLoggerService<T>
    {
        private readonly ILogger _logger;
        public AppLoggerService(ILogger logger)
        {
            _logger = logger.ForContext<T>();
        }
        public void LogInformation(string message, params object[] args)
        {
            _logger.Information(message, args);
        }
        public void LogWarning(string message, params object[] args)
        {
            _logger.Warning(message, args);
        }
        public void LogError(string message, params object[] args)
        {
            _logger.Error(message, args);
        }
        public void LogError(Exception ex, string v)
        {
            _logger.Error(ex, v);
        }
    }
}
