using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        public void ShouldGetFeedUrlValueFromHttpRequestParametersUsingParser()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format("http://www.something.com/api/resource?feedurl={0}", FeedUrl))
            };

            var obtained = _requestParametersParser.Parse(httpRequestMessage);

            Assert.AreEqual(FeedUrl, obtained.FeedUrl);
        }

        [TestMethod]
        public void ShouldGetLimitValueFromHttpRequestParametersUsingParser()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format("http://www.something.com/api/resource?limit={0}", Limit))
            };

            var obtained = _requestParametersParser.Parse(httpRequestMessage);

            Assert.AreEqual(Limit, obtained.Limit.ToString());
        }

        [TestMethod]
        public void ShouldGetOffsetValueFromHttpRequestParametersUsingParser()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format("http://www.something.com/api/resource?offset={0}", Offset))
            };

            var obtained = _requestParametersParser.Parse(httpRequestMessage);

            Assert.AreEqual(Offset, obtained.Offset.ToString());
        }

        [TestMethod]
        public void ShouldGetValuesAndIgnoreCaseFromHttpRequestParametersUsingParser()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(string.Format("http://www.something.com/api/resource?FEedUrL={0}&LIMIT={1}&offSET={2}", FeedUrl, Limit, Offset))
            };

            var obtained = _requestParametersParser.Parse(httpRequestMessage);

            Assert.AreEqual(FeedUrl, obtained.FeedUrl);
            Assert.AreEqual(Limit, obtained.Limit.ToString());
            Assert.AreEqual(Offset, obtained.Offset.ToString());
        }
    }
}
