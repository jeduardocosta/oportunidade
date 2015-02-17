using System;
using NLog;
using MinutoSeguros.BlogFeed.Log.NLog;

namespace MinutoSeguros.BlogFeed.Log
{
    public class LoggerWrapper : ILogger
    {
        private static Logger _logger;
        
        private static Logger Logger
        {
            get
            {
                return _logger ?? LogManager.GetCurrentClassLogger();
            }
        }
        
        private readonly NLog.ILogger _nlogLogger;

        public LoggerWrapper()
        {
            _nlogLogger = new LoggerAdapter(Logger);
        }

        public void Info(string message)
        {
            _nlogLogger.Info(message);
        }

        public void Error(string message)
        {
            _nlogLogger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            _nlogLogger.Error(message, exception);
        }
    }
}