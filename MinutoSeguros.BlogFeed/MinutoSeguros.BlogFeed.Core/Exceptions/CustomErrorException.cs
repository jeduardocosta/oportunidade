using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.Core.Exceptions
{
    [Serializable]
    public class CustomErrorException : Exception
    {
        public CustomErrorException(string message) : base(message) { }
    }
}