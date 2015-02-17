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
        public void ShouldCreateRequestParametersObject()
        {
            const string feedUrl = "http://www.feedurl.com/blog/api/sample";
            const string limit = "5";
            const string offset = "1";
            const string numberOfTopWords = "15";

            var requestParameters = new RequestParameters(feedUrl, limit, offset, numberOfTopWords);

            Assert.AreEqual(feedUrl, requestParameters.FeedUrl);
            Assert.AreEqual(int.Parse(limit), requestParameters.Limit);
            Assert.AreEqual(int.Parse(offset), requestParameters.Offset);
            Assert.AreEqual(int.Parse(numberOfTopWords), requestParameters.NumberOfTopWords);
        }

        [TestMethod]
        public void ShouldCreateRequestParametersObjectAndGetLimitMaximumAllowedValue()
        {
            const int expected = 10;
            const string limitValue = "20";

            var requestParameters = new RequestParameters(It.IsAny<string>(), limitValue, It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, requestParameters.Limit);
        }

        [TestMethod]
        public void ShouldCreateRequestParametersObjectAndGetLimitDefaultValue()
        {
            const int expected = 10;
            const string withoutLimitValue = null;

            var requestParameters = new RequestParameters(It.IsAny<string>(), withoutLimitValue, It.IsAny<string>(), It.IsAny<string>());

            Assert.AreEqual(expected, requestParameters.Limit);
        }

        [TestMethod]
        public void ShouldCreateRequestParametersObjectAndGetOffsetDefaultValue()
        {
            const int expected = 0;
            const string withoutOffsetValue = null;

            var requestParameters = new RequestParameters(It.IsAny<string>(), It.IsAny<string>(), withoutOffsetValue, It.IsAny<string>());

            Assert.AreEqual(expected, requestParameters.Offset);
        }

        [TestMethod]
        public void ShouldCreateRequestParametersObjectAndGetNumberOfTopWordsDefaultValue()
        {
            const int expected = 10;
            const string numberOfTopWordsValue = null;

            var requestParameters = new RequestParameters(It.IsAny<string>(), It.IsAny<string>(), numberOfTopWordsValue, It.IsAny<string>());

            Assert.AreEqual(expected, requestParameters.NumberOfTopWords);
        }
    }
}
