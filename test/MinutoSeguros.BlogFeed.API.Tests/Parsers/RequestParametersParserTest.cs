using NUnit.Framework;
using MinutoSeguros.BlogFeed.API.Parsers;
using System;
using System.Net.Http;
using FluentAssertions;

namespace MinutoSeguros.BlogFeed.API.Tests.Parsers
{
    [TestFixture]
    public class RequestParametersParserTest
    {
        private IRequestParametersParser _requestParametersParser;
        private HttpRequestMessage _httpRequestMessage;

        private const string FeedUrl = "http://www.blog.com/feed";
        private const string Limit = "8";
        private const string Offset = "2";

        [SetUp]
        public void SetUp()
        {
            _requestParametersParser = new RequestParametersParser();
        }

        [Test]
        public void Should_GetFeedUrlValue_FromHttpRequestParameters_UsingParser()
        {
            _httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"http://www.something.com/api/resource?feedurl={FeedUrl}")
            };

            var obtained = _requestParametersParser.Parse(_httpRequestMessage);

            obtained.FeedUrl.Should().Be(FeedUrl);
        }

        [Test]
        public void Should_GetLimitValue_FromHttpRequestParameters_UsingParser()
        {
            _httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"http://www.something.com/api/resource?limit={Limit}")
            };

            var obtained = _requestParametersParser.Parse(_httpRequestMessage);

            obtained.Limit.ToString().Should().Be(Limit);
        }

        [Test]
        public void Should_GetOffsetValue_FromHttpRequestParameters_UsingParser()
        {
            _httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"http://www.something.com/api/resource?offset={Offset}")
            };

            var obtained = _requestParametersParser.Parse(_httpRequestMessage);

            obtained.Offset.ToString().Should().Be(Offset);
        }

        [Test]
        public void Should_GetFeedUrlValueAndIgnoreCase_FromHttpRequestParameters_UsingParser()
        {
            _httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"http://www.something.com/api/resource?FEedUrL={FeedUrl}&LIMIT={Limit}&offSET={Offset}")
            };

            var obtained = _requestParametersParser.Parse(_httpRequestMessage);

            obtained.FeedUrl.Should().Be(FeedUrl);
        }

        [Test]
        public void Should_GetLimitValueAndIgnoreCase_FromHttpRequestParameters_UsingParser()
        {
            _httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"http://www.something.com/api/resource?FEedUrL={FeedUrl}&LIMIT={Limit}&offSET={Offset}")
            };

            var obtained = _requestParametersParser.Parse(_httpRequestMessage);

            obtained.Limit.ToString().Should().Be(Limit);
        }

        [Test]
        public void Should_GetOffsetValueAndIgnoreCase_FromHttpRequestParameters_UsingParser()
        {
            _httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"http://www.something.com/api/resource?FEedUrL={FeedUrl}&LIMIT={Limit}&offSET={Offset}")
            };

            var obtained = _requestParametersParser.Parse(_httpRequestMessage);

            obtained.Offset.ToString().Should().Be(Offset);
        }
    }
}