using System;

namespace MinutoSeguros.BlogFeed.Log
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message);
        void Error(string message, Exception exception);
    }
}