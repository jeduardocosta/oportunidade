using NUnit.Framework;
using MinutoSeguros.BlogFeed.API.Models;
using Moq;
using FluentAssertions;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestFixture]
    public class RequestParametersTest
    {
        [Test]
        public void Should_CreateRequestParametersObject_AndCheckFeedUrlParameter()
        {
            const string feedUrl = "http://www.feedurl.com/blog/api/sample";

            var requestParameters = new RequestParameters(feedUrl, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            requestParameters.FeedUrl.Should().Be(feedUrl);
        }

        [Test]
        public void Should_CreateRequestParametersObject_AndCheckLimitParameter()
        {
            const string limit = "5";

            var requestParameters = new RequestParameters(It.IsAny<string>(), limit, It.IsAny<string>(), It.IsAny<string>());

            requestParameters.Limit.Should().Be(int.Parse(limit));
        }

        [Test]
        public void Should_CreateRequestParametersObject_AndCheckOffsetParameter()
        {
            const string offset = "1";

            var requestParameters = new RequestParameters(It.IsAny<string>(), It.IsAny<string>(), offset, It.IsAny<string>());

            requestParameters.Offset.Should().Be(int.Parse(offset));
        }

        [Test]
        public void Should_CreateRequestParametersObject_AndCheckNumberOfTopWordsParameter()
        {
            const string numberOfTopWords = "15";

            var requestParameters = new RequestParameters(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), numberOfTopWords);

            requestParameters.NumberOfTopWords.Should().Be(int.Parse(numberOfTopWords));
        }

        [Test]
        public void Should_CreateRequestParametersObject_AndGetLimitMaximumAllowedValue()
        {
            const int expected = 10;
            const string limitValue = "20";

            var requestParameters = new RequestParameters(It.IsAny<string>(), limitValue, It.IsAny<string>(), It.IsAny<string>());

            requestParameters.Limit.Should().Be(expected);
        }

        [Test]
        public void Should_CreateRequestParametersObject_AndGetLimitDefaultValue()
        {
            const int expected = 10;
            const string withoutLimitValue = null;

            var requestParameters = new RequestParameters(It.IsAny<string>(), withoutLimitValue, It.IsAny<string>(), It.IsAny<string>());

            requestParameters.Limit.Should().Be(expected);
        }

        [Test]
        public void Should_CreateRequestParametersObject_AndGetOffsetDefaultValue()
        {
            const int expected = 0;
            const string withoutOffsetValue = null;

            var requestParameters = new RequestParameters(It.IsAny<string>(), It.IsAny<string>(), withoutOffsetValue, It.IsAny<string>());

            requestParameters.Offset.Should().Be(expected);
        }

        [Test]
        public void Should_CreateRequestParametersObject_AndGetNumberOfTopWordsDefaultValue()
        {
            const int expected = 10;
            const string numberOfTopWordsValue = null;

            var requestParameters = new RequestParameters(It.IsAny<string>(), It.IsAny<string>(), numberOfTopWordsValue, It.IsAny<string>());

            requestParameters.NumberOfTopWords.Should().Be(expected);
        }
    }
}