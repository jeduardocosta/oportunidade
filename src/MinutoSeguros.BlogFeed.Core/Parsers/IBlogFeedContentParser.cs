using System.Collections.Generic;
using System.ServiceModel.Syndication;
using MinutoSeguros.BlogFeed.Core.Entities;

namespace MinutoSeguros.BlogFeed.Core.Parsers
{
    public interface IBlogFeedContentParser
    {
        IEnumerable<BlogFeedContent> Parse(SyndicationFeed feedContent);
    }
}