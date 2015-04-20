using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.Core.Entities;
using MinutoSeguros.BlogFeed.Core.Helpers;
using MinutoSeguros.BlogFeed.Core.Parsers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.Core.Tests.Parsers
{
    [TestClass]
    public class BlogFeedContentParserTest
    {
        private Mock<IHtmlHelper> _mockHtmlHelper;
        private IBlogFeedContentParser _blogFeedContentParser;

        private const string SampleTitle = "sample title";
        private const string SampleCategory = "sample category";
        private DateTime SamplePublishDate = DateTime.Now;
        private string SampleHtmlContent = "sample html content";
        
        [TestInitialize]
        public void Init()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();

            _mockHtmlHelper
                .Setup(it => it.RemoveTags(It.IsAny<string>()))
                .Returns(SampleHtmlContent);

            _blogFeedContentParser = new BlogFeedContentParser(_mockHtmlHelper.Object);
        }

        [TestMethod]
        public void Should_ParseSyndicationFeed_UsingBlogFeedContentParser()
        {
            var syndicationFeed = GivenASyndicationFeedObject();

            var expected = new List<BlogFeedContent> 
            { 
                new BlogFeedContent(SampleTitle, SamplePublishDate, new List<string>(), SampleHtmlContent) 
            };

            var obtained = _blogFeedContentParser.Parse(syndicationFeed);

            _mockHtmlHelper.Verify(it => it.RemoveTags(It.IsAny<string>()), Times.Once);

            Assert.IsTrue(AreEqual(expected.First(), obtained.First()));
        }

        [TestMethod]
        public void Should_ParseSyndicationFeed_UsingBlogFeedContentParser_FromMultipleRecords()
        {
            const int syndicationItemsLength = 10;
            var syndicationFeed = GivenASyndicationFeedObjectWithMultipleItems(syndicationItemsLength);

            var obtained = _blogFeedContentParser.Parse(syndicationFeed);

            _mockHtmlHelper.Verify(it => it.RemoveTags(It.IsAny<string>()), Times.Exactly(syndicationItemsLength));

            Assert.AreEqual(syndicationItemsLength, obtained.Count());
        }

        private SyndicationFeed GivenASyndicationFeedObject(int syndicationItemsLength = 1)
        {
            var itemAlternateLink = new Uri("http://www.sample.com");

            var syndicationFeedItem = new SyndicationItem(SampleTitle, SampleHtmlContent, itemAlternateLink);
            syndicationFeedItem.PublishDate = SamplePublishDate;

            var syndicationItems = new List<SyndicationItem>();

            for (var index = 0; index < syndicationItemsLength; index++)
                syndicationItems.Add(syndicationFeedItem);

            return new SyndicationFeed { Items = syndicationItems };
        }

        private SyndicationFeed GivenASyndicationFeedObjectWithMultipleItems(int syndicationItemsLength)
        {
            return GivenASyndicationFeedObject(syndicationItemsLength);
        }

        private bool AreEqual(BlogFeedContent source, BlogFeedContent destination)
        {
            return source.Categories.SequenceEqual(destination.Categories) &&
                   source.FullContent == destination.FullContent &&
                   source.PublishDate == destination.PublishDate &&
                   source.Title == destination.Title &&
                   source.TopWords.SequenceEqual(destination.TopWords);
        }
    }
}