using System;

namespace MinutoSeguros.BlogFeed.Core.Exceptions
{
    [Serializable]
    public class CustomErrorException : Exception
    {
        public CustomErrorException(string message) : base(message) { }
    }
}