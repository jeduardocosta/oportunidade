using System.Collections.Generic;
using MinutoSeguros.BlogFeed.Core.Entities;

namespace MinutoSeguros.BlogFeed.Core.Readers
{
    public interface IBlogFeedReader
    {
        IEnumerable<BlogFeedContent> Read(string feedUrl);
    }
}