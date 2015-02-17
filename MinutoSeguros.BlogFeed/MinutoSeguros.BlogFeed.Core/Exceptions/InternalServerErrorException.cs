using System;

namespace MinutoSeguros.BlogFeed.Core.Exceptions
{
    [Serializable]
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException() : base("Internal server error.") { }
    }
}