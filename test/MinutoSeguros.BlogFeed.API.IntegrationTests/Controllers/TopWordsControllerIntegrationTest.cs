using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Controllers;
using MinutoSeguros.BlogFeed.API.DI;
using MinutoSeguros.BlogFeed.API.Models;

namespace MinutoSeguros.BlogFeed.API.IntegrationTests.Controllers
{
    [TestClass]
    public class TopWordsControllerIntegrationTest
    {
        private TopWordsController _controller;

        private const string FeedUrl = "http://www.minutoseguros.com.br/blog/feed/";

        private int _limit;
        private int _offset;

        [TestInitialize]
        public void Init()
        {
            _limit = 4;
            _offset = 6;

            _controller = DIContainer.Resolve<TopWordsController>();
            _controller.Request = new HttpRequestMessage();
        }

        [TestMethod]
        public void Should_GetData_FromTopWordsController()
        {
            _controller.Request.RequestUri = new Uri(string.Format("http://localhost.com/topWords?feedurl={0}", FeedUrl));

            var obtained = _controller.Get();

            var topWordsResponseResult = obtained as OkNegotiatedContentResult<TopWordsResponse>;

            Assert.IsTrue(topWordsResponseResult.Content.TopWords.Any());
        }

        [TestMethod]
        public void Should_GetMetadataLimitValue_FromTopWordsController()
        {
            _controller.Request.RequestUri = new Uri(string.Format("http://localhost.com/topWords?feedurl={0}&limit={1}&offset={2}", FeedUrl, _limit, _offset));

            var obtained = _controller.Get();

            var topWordsResponseResult = obtained as OkNegotiatedContentResult<TopWordsResponse>;

            Assert.AreEqual(_limit, topWordsResponseResult.Content.Metadata.Limit);
        }

        [TestMethod]
        public void Should_GetMetadataOffsetValue_FromTopWordsController()
        {
            _controller.Request.RequestUri = new Uri(string.Format("http://localhost.com/topWords?feedurl={0}&limit={1}&offset={2}", FeedUrl, _limit, _offset));

            var obtained = _controller.Get();

            var topWordsResponseResult = obtained as OkNegotiatedContentResult<TopWordsResponse>;

            Assert.AreEqual(_offset, topWordsResponseResult.Content.Metadata.Offset);
        }
    }
}