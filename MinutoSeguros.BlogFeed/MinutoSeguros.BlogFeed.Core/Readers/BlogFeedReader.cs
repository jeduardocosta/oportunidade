using MinutoSeguros.BlogFeed.Core.Entities;
using MinutoSeguros.BlogFeed.Core.Exceptions;
using MinutoSeguros.BlogFeed.Core.Helpers;
using MinutoSeguros.BlogFeed.Core.Parsers;
using MinutoSeguros.BlogFeed.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace MinutoSeguros.BlogFeed.Core.Readers
{
    public interface IBlogFeedReader
    {
        IEnumerable<BlogFeedContent> Read(string feedUrl);
    }

    public class BlogFeedReader : IBlogFeedReader
    {
        private readonly IBlogFeedContentParser _blogFeedContentParser;
        private readonly IUrlHelper _urlHelper;
        private readonly ILogger _logger;

        public BlogFeedReader(IBlogFeedContentParser blogFeedContentParser, IUrlHelper urlHelper, ILogger logger)
        {
            _blogFeedContentParser = blogFeedContentParser;
            _urlHelper = urlHelper;
            _logger = logger;
        }

        public IEnumerable<BlogFeedContent> Read(string feedUrl)
        {
            ValidateFeedUrl(feedUrl);

            try
            {
                var reader = GetXmlReader(feedUrl);
                var feed = GetSyndicationFeedLoad(reader);
                var blogFeedContent = _blogFeedContentParser.Parse(feed);
                return blogFeedContent;
            }
            catch (Exception exception)
            {
                _logger.Error(string.Format("failed to read blog feed. Entry url: {0}.", feedUrl), exception);
                throw;
            }
        }

        protected virtual SyndicationFeed GetSyndicationFeedLoad(XmlReader reader)
        {
            return SyndicationFeed.Load(reader);
        }

        protected virtual XmlReader GetXmlReader(string feedUrl)
        {
            return XmlReader.Create(feedUrl);
        }

        private void ValidateFeedUrl(string feedUrl)
        {
            var isValidUrl = _urlHelper.IsValidAbsoluteUrl(feedUrl);

            if (!isValidUrl)
                throw new CustomErrorException(string.Format("invalid entry blog feed absolute url. value: {0}.", feedUrl));
        }
    }
}