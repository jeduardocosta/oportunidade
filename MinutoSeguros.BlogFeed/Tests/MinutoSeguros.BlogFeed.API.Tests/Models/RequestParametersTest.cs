using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestClass]
    public class RequestParametersTest
    {
        [TestMethod]
        public void Should_CreateRequestParametersObject_AndCheckFeedUrlParameter()
        {
            const string feedUrl = "http://www.feedurl.com/blog/api/sample";

            var requestParameters = new RequestParameters(feedUrl, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(feedUrl, requestParameters.FeedUrl);
        }

        [TestMethod]
        public void Should_CreateRequestParametersObject_AndCheckLimitParameter()
        {
            const string limit = "5";

            var requestParameters = new RequestParameters(It.IsAny<string>(), limit, It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(int.Parse(limit), requestParameters.Limit);
        }

        [TestMethod]
        public void Should_CreateRequestParametersObject_AndCheckOffsetParameter()
        {
            const string offset = "1";

            var requestParameters = new RequestParameters(It.IsAny<string>(), It.IsAny<string>(), offset, It.IsAny<string>());

            Assert.AreEqual(int.Parse(offset), requestParameters.Offset);
        }

        [TestMethod]
        public void Should_CreateRequestParametersObject_AndCheckNumberOfTopWordsParameter()
        {
            const string numberOfTopWords = "15";

            var requestParameters = new RequestParameters(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), numberOfTopWords);

            Assert.AreEqual(int.Parse(numberOfTopWords), requestParameters.NumberOfTopWords);
        }

        [TestMethod]
        public void Should_CreateRequestParametersObject_AndGetLimitMaximumAllowedValue()
        {
            const int expected = 10;
            const string limitValue = "20";

            var requestParameters = new RequestParameters(It.IsAny<string>(), limitValue, It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, requestParameters.Limit);
        }

        [TestMethod]
        public void Should_CreateRequestParametersObject_AndGetLimitDefaultValue()
        {
            const int expected = 10;
            const string withoutLimitValue = null;

            var requestParameters = new RequestParameters(It.IsAny<string>(), withoutLimitValue, It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, requestParameters.Limit);
        }

        [TestMethod]
        public void Should_CreateRequestParametersObject_AndGetOffsetDefaultValue()
        {
            const int expected = 0;
            const string withoutOffsetValue = null;

            var requestParameters = new RequestParameters(It.IsAny<string>(), It.IsAny<string>(), withoutOffsetValue, It.IsAny<string>());

            Assert.AreEqual(expected, requestParameters.Offset);
        }

        [TestMethod]
        public void Should_CreateRequestParametersObject_AndGetNumberOfTopWordsDefaultValue()
        {
            const int expected = 10;
            const string numberOfTopWordsValue = null;

            var requestParameters = new RequestParameters(It.IsAny<string>(), It.IsAny<string>(), numberOfTopWordsValue, It.IsAny<string>());

            Assert.AreEqual(expected, requestParameters.NumberOfTopWords);
        }
    }
}