using NUnit.Framework;
using MinutoSeguros.BlogFeed.Core.Entities;
using MinutoSeguros.BlogFeed.Core.Helpers;
using MinutoSeguros.BlogFeed.Core.Parsers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using FluentAssertions;

namespace MinutoSeguros.BlogFeed.Core.Tests.Parsers
{
    [TestFixture]
    public class BlogFeedContentParserTest
    {
        private Mock<IHtmlHelper> _mockHtmlHelper;
        private IBlogFeedContentParser _blogFeedContentParser;

        private const string SampleTitle = "sample title";
        private readonly DateTime _samplePublishDate = DateTime.Now;
        private readonly string SampleHtmlContent = "sample html content";
        
        [SetUp]
        public void SetUp()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();

            _mockHtmlHelper
                .Setup(it => it.RemoveTags(It.IsAny<string>()))
                .Returns(SampleHtmlContent);

            _blogFeedContentParser = new BlogFeedContentParser(_mockHtmlHelper.Object);
        }

        [Test]
        public void Should_ParseSyndicationFeed_UsingBlogFeedContentParser()
        {
            var syndicationFeed = GivenSyndicationFeedObject();

            var expected = new List<BlogFeedContent> 
            { 
                new BlogFeedContent(SampleTitle, _samplePublishDate, new List<string>(), SampleHtmlContent) 
            };

            var obtained = _blogFeedContentParser.Parse(syndicationFeed);

            _mockHtmlHelper.Verify(it => it.RemoveTags(It.IsAny<string>()), Times.Once);

            AreEqual(obtained.First(), expected.First()).Should().BeTrue();
        }

        [Test]
        public void Should_ParseSyndicationFeed_UsingBlogFeedContentParser_FromMultipleRecords()
        {
            const int syndicationItemsLength = 10;
            var syndicationFeed = GivenSyndicationFeedObjectWithMultipleItems(syndicationItemsLength);

            var obtained = _blogFeedContentParser.Parse(syndicationFeed);

            _mockHtmlHelper.Verify(it => it.RemoveTags(It.IsAny<string>()), Times.Exactly(syndicationItemsLength));

            obtained
                .Count()
                .Should()
                .Be(syndicationItemsLength);
        }

        private SyndicationFeed GivenSyndicationFeedObject(int syndicationItemsLength = 1)
        {
            var itemAlternateLink = new Uri("http://www.sample.com");

            var syndicationFeedItem = new SyndicationItem(SampleTitle, SampleHtmlContent, itemAlternateLink)
            {
                PublishDate = _samplePublishDate
            };

            var syndicationItems = new List<SyndicationItem>();

            for (var index = 0; index < syndicationItemsLength; index++)
            {
                syndicationItems.Add(syndicationFeedItem);
            }

            return new SyndicationFeed { Items = syndicationItems };
        }

        private SyndicationFeed GivenSyndicationFeedObjectWithMultipleItems(int syndicationItemsLength)
        {
            return GivenSyndicationFeedObject(syndicationItemsLength);
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