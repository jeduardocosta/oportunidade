using MinutoSeguros.BlogFeed.Core.Entities;
using MinutoSeguros.BlogFeed.Core.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.Core.Parsers
{
    public interface IBlogFeedContentParser
    {
        IEnumerable<BlogFeedContent> Parse(SyndicationFeed feedContent);
    }

    public class BlogFeedContentParser : IBlogFeedContentParser
    {
        private IHtmlHelper _htmlHelper;

        public BlogFeedContentParser(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public IEnumerable<BlogFeedContent> Parse(SyndicationFeed feed)
        {
            var blogFeedContents = new List<BlogFeedContent>();

            foreach (var feedItem in feed.Items)
            {
                var title = feedItem.Title.Text;
                var publishDate = feedItem.PublishDate.DateTime;
                var categories = feedItem.Categories.Select(it => it.Name);
                var htmlContent = ExtractContent(feedItem);
                var content = _htmlHelper.RemoveTags(htmlContent);

                var blogFeedContentItem = new BlogFeedContent(title, publishDate, categories, content);
                blogFeedContents.Add(blogFeedContentItem);
            };

            return blogFeedContents;
        }

        private string ExtractContent(SyndicationItem syndicationItem)
        {
            var content = syndicationItem
                    .ElementExtensions
                    .ReadElementExtensions<string>("encoded", "http://purl.org/rss/1.0/modules/content/")
                    .FirstOrDefault();

            return content;
        }
    }
}