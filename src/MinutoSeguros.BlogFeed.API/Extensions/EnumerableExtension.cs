using System.Collections.Generic;
using System.Linq;

namespace MinutoSeguros.BlogFeed.API.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> Pagination<T>(this IEnumerable<T> data, int limit, int offset)
        {
            return data.Skip(offset * limit).Take(limit);
        }
    }
}