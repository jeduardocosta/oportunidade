using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MinutoSeguros.BlogFeed.API.Tests.Parsers
{
    [TestClass]
    public class RequestParametersParserTest
    {
        private IRequestParametersParser _requestParametersParser;

        private const string FeedUrl = "http://www.blog.com/feed";
        private const string Limit = "8";
        private const string Offset = "2";

        [TestInitialize]
        public void Init()
        {
            _requestParametersParser = new RequestParametersParser();
        }

        [TestMethod]
        public void Should_GetFeedUrlValue_FromHttpRequestParameters_UsingParser()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format("http://www.something.com/api/resource?feedurl={0}", FeedUrl))
            };

            var obtained = _requestParametersParser.Parse(httpRequestMessage);

            Assert.AreEqual(FeedUrl, obtained.FeedUrl);
        }

        [TestMethod]
        public void Should_GetLimitValue_FromHttpRequestParameters_UsingParser()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format("http://www.something.com/api/resource?limit={0}", Limit))
            };

            var obtained = _requestParametersParser.Parse(httpRequestMessage);

            Assert.AreEqual(Limit, obtained.Limit.ToString());
        }

        [TestMethod]
        public void Should_GetOffsetValue_FromHttpRequestParameters_UsingParser()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format("http://www.something.com/api/resource?offset={0}", Offset))
            };

            var obtained = _requestParametersParser.Parse(httpRequestMessage);

            Assert.AreEqual(Offset, obtained.Offset.ToString());
        }

        [TestMethod]
        public void Should_GetFeedUrlValueAndIgnoreCase_FromHttpRequestParameters_UsingParser()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format("http://www.something.com/api/resource?FEedUrL={0}&LIMIT={1}&offSET={2}", FeedUrl, Limit, Offset))
            };

            var obtained = _requestParametersParser.Parse(httpRequestMessage);

            Assert.AreEqual(FeedUrl, obtained.FeedUrl);
        }

        [TestMethod]
        public void Should_GetLimitValueAndIgnoreCase_FromHttpRequestParameters_UsingParser()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format("http://www.something.com/api/resource?FEedUrL={0}&LIMIT={1}&offSET={2}", FeedUrl, Limit, Offset))
            };

            var obtained = _requestParametersParser.Parse(httpRequestMessage);

            Assert.AreEqual(Limit, obtained.Limit.ToString());
        }

        [TestMethod]
        public void Should_GetOffsetValueAndIgnoreCase_FromHttpRequestParameters_UsingParser()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format("http://www.something.com/api/resource?FEedUrL={0}&LIMIT={1}&offSET={2}", FeedUrl, Limit, Offset))
            };

            var obtained = _requestParametersParser.Parse(httpRequestMessage);

            Assert.AreEqual(Offset, obtained.Offset.ToString());
        }
    }
}
