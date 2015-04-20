using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.Core.Readers;
using Moq;
using MinutoSeguros.BlogFeed.Core.Parsers;
using MinutoSeguros.BlogFeed.Core.Helpers;
using MinutoSeguros.BlogFeed.Core.Exceptions;
using System.ServiceModel.Syndication;
using MinutoSeguros.BlogFeed.Core.Entities;
using System.Collections.Generic;
using MinutoSeguros.BlogFeed.Log;

namespace MinutoSeguros.BlogFeed.Core.Tests.Readers
{
    [TestClass]
    public class BlogFeedReaderTest
    {
        private Mock<IBlogFeedContentParser> _mockBlogFeedContentParser;
        private Mock<IUrlHelper> _mockUrlHelper;
        private Mock<ILogger> _mockLogger;

        private IBlogFeedReader _blogFeedReader;

        const string InvalidFeedUrl = "domain.com/blog/feed";
        const string ValidFeedUrl = "http://www.minutoseguros.com.br/blog/feed/";

        [TestInitialize]
        public void Init()
        {
            _mockBlogFeedContentParser = new Mock<IBlogFeedContentParser>();
            _mockUrlHelper = new Mock<IUrlHelper>();
            _mockLogger = new Mock<ILogger>();

            _mockBlogFeedContentParser
                .Setup(it => it.Parse(It.IsAny<SyndicationFeed>()))
                .Returns(GivenAnSetOfBlogFeedContent());
            
            _mockUrlHelper
                .Setup(it => it.IsValidAbsoluteUrl(InvalidFeedUrl))
                .Returns(false);

            _mockUrlHelper
                .Setup(it => it.IsValidAbsoluteUrl(ValidFeedUrl))
                .Returns(true);

            _blogFeedReader = new BlogFeedReader(_mockBlogFeedContentParser.Object, _mockUrlHelper.Object, _mockLogger.Object);
        }

        [TestMethod]
        public void Should_ReadBlogFeedContent_UsingBlogFeedReader()
        {
            var obtained = _blogFeedReader.Read(ValidFeedUrl);

            _mockUrlHelper.Verify(it => it.IsValidAbsoluteUrl(ValidFeedUrl), Times.Once);
            _mockUrlHelper.Verify(it => it.IsValidAbsoluteUrl(InvalidFeedUrl), Times.Never);

            _mockBlogFeedContentParser.Verify(it => it.Parse(It.IsAny<SyndicationFeed>()), Times.Once);

            Assert.IsNotNull(obtained);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomErrorException), AllowDerivedTypes = false)]
        public void Should_ThrowException_WhenReadFeedUrl_WithInvalidEntryUrl()
        {
            var obtained = _blogFeedReader.Read(InvalidFeedUrl);
        }

        private IEnumerable<BlogFeedContent> GivenAnSetOfBlogFeedContent()
        {
            return new List<BlogFeedContent>
            {
                new BlogFeedContent("title", DateTime.Now, new [] {"category"}, "content")
            };
        }
    }
}