using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Controllers;
using MinutoSeguros.BlogFeed.API.DI;
using MinutoSeguros.BlogFeed.API.Models;
using MinutoSeguros.BlogFeed.API.Parsers;
using MinutoSeguros.BlogFeed.Core.Entities;
using MinutoSeguros.BlogFeed.Core.Exceptions;
using MinutoSeguros.BlogFeed.Core.Readers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace MinutoSeguros.BlogFeed.API.Tests.Controllers
{
    [TestClass]
    public class TopWordsControllerIntegrationTest
    {
        private TopWordsController _controller;

        private const string FeedUrl = "http://www.minutoseguros.com.br/blog/feed/";

        [TestInitialize]
        public void Init()
        {
            _controller = DIContainer.Resolve<TopWordsController>();
            _controller.Request = new HttpRequestMessage();
        }

        [TestMethod]
        public void ShouldGetTopWordsContentInTopWordsControllerWithIntegration()
        {
            _controller.Request.RequestUri = new Uri(string.Format("http://localhost.com/topWords?feedurl={0}", FeedUrl));

            var obtained = _controller.Get();

            var topWordsResponseResult = obtained as OkNegotiatedContentResult<TopWordsResponse>;

            Assert.IsTrue(topWordsResponseResult.Content.Metadata.Limit > 0);
            Assert.IsTrue(topWordsResponseResult.Content.TopWords.Any());
        }

        [TestMethod]
        public void ShouldGetTopWordsWithPaginationParametersContentInTopWordsControllerWithIntegration()
        {
            const int limit = 4;
            const int offset = 6;

            _controller.Request.RequestUri = new Uri(string.Format("http://localhost.com/topWords?feedurl={0}&limit={1}&offset={2}", FeedUrl, limit, offset));

            var obtained = _controller.Get();

            var topWordsResponseResult = obtained as OkNegotiatedContentResult<TopWordsResponse>;

            Assert.AreEqual(limit, topWordsResponseResult.Content.Metadata.Limit);
            Assert.AreEqual(offset, topWordsResponseResult.Content.Metadata.Offset);
            Assert.AreEqual(limit, topWordsResponseResult.Content.TopWords.Count());
        }
    }
}