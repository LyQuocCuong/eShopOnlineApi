using Contracts.Utilities.Logger;
using NLog;

namespace eShopOnlineUtilities.NLog
{
    public sealed class NLogService : ILogService
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }
    }
}
