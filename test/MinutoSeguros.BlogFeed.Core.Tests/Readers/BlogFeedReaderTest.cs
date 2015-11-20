using System;
using NUnit.Framework;
using MinutoSeguros.BlogFeed.Core.Readers;
using Moq;
using MinutoSeguros.BlogFeed.Core.Parsers;
using MinutoSeguros.BlogFeed.Core.Helpers;
using MinutoSeguros.BlogFeed.Core.Exceptions;
using System.ServiceModel.Syndication;
using MinutoSeguros.BlogFeed.Core.Entities;
using System.Collections.Generic;
using FluentAssertions;
using MinutoSeguros.BlogFeed.Log;

namespace MinutoSeguros.BlogFeed.Core.Tests.Readers
{
    [TestFixture]
    public class BlogFeedReaderTest
    {
        private Mock<IBlogFeedContentParser> _mockBlogFeedContentParser;
        private Mock<IUrlHelper> _mockUrlHelper;
        private Mock<ILogger> _mockLogger;

        private IBlogFeedReader _blogFeedReader;

        const string InvalidFeedUrl = "domain.com/blog/feed";
        const string ValidFeedUrl = "http://www.minutoseguros.com.br/blog/feed/";

        [SetUp]
        public void SetUp()
        {
            _mockBlogFeedContentParser = new Mock<IBlogFeedContentParser>();
            _mockUrlHelper = new Mock<IUrlHelper>();
            _mockLogger = new Mock<ILogger>();

            _mockBlogFeedContentParser
                .Setup(it => it.Parse(It.IsAny<SyndicationFeed>()))
                .Returns(GivenSetOfBlogFeedContent());
            
            _mockUrlHelper
                .Setup(it => it.IsValidAbsoluteUrl(InvalidFeedUrl))
                .Returns(false);

            _mockUrlHelper
                .Setup(it => it.IsValidAbsoluteUrl(ValidFeedUrl))
                .Returns(true);

            _blogFeedReader = new BlogFeedReader(_mockBlogFeedContentParser.Object, _mockUrlHelper.Object, _mockLogger.Object);
        }

        [Test]
        public void Should_ReturnValidSetOfBlogFeedContent_UsingBlogFeedReader()
        {
            _blogFeedReader
                .Read(ValidFeedUrl)
                .Should()
                .NotBeNull();
        }

        [Test]
        public void Should_ReadBlogFeedContent_UsingBlogFeedReader()
        {
            _blogFeedReader.Read(ValidFeedUrl);

            _mockUrlHelper.Verify(it => it.IsValidAbsoluteUrl(ValidFeedUrl), Times.Once);
            _mockUrlHelper.Verify(it => it.IsValidAbsoluteUrl(InvalidFeedUrl), Times.Never);
            _mockBlogFeedContentParser.Verify(it => it.Parse(It.IsAny<SyndicationFeed>()), Times.Once);

        }

        [Test]
        public void Should_ThrowException_WhenReadFeedUrl_WithInvalidEntryUrl()
        {
            Action action = () => _blogFeedReader.Read(InvalidFeedUrl);

            action
                .ShouldThrow<CustomErrorException>();
        }

        private IEnumerable<BlogFeedContent> GivenSetOfBlogFeedContent()
        {
            return new List<BlogFeedContent>
            {
                new BlogFeedContent("title", DateTime.Now, new [] {"category"}, "content")
            };
        }
    }
}