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
    public class PostsControllerIntegrationTest
    {
        private PostsController _controller;

        private const string FeedUrl = "http://www.minutoseguros.com.br/blog/feed/";

        private int _limit;
        private int _offset;

        [TestInitialize]
        public void Init()
        {
            _limit = 5;
            _offset = 1;

            _controller = DIContainer.Resolve<PostsController>();
            _controller.Request = new HttpRequestMessage();
        }

        [TestMethod]
        public void Should_GetData_FromPostsController()
        {
            _controller.Request.RequestUri = new Uri(string.Format("http://localhost.com/posts?feedurl={0}", FeedUrl));

            var obtained = _controller.Get();

            var postResponseResult = obtained as OkNegotiatedContentResult<PostResponse>;

            Assert.IsTrue(postResponseResult.Content.Posts.Any());
        }

        [TestMethod]
        public void Should_GetMetadataLimitValue_FromPostsController()
        {
            _controller.Request.RequestUri = new Uri(string.Format("http://localhost.com/posts?feedurl={0}&limit={1}&offset={2}", FeedUrl, _limit, _offset));

            var obtained = _controller.Get();

            var postResponseResult = obtained as OkNegotiatedContentResult<PostResponse>;

            Assert.AreEqual(_limit, postResponseResult.Content.Metadata.Limit);
        }

        [TestMethod]
        public void Should_GetMetadataOffsetValue_FromPostsController()
        {
            _controller.Request.RequestUri = new Uri(string.Format("http://localhost.com/posts?feedurl={0}&limit={1}&offset={2}", FeedUrl, _limit, _offset));

            var obtained = _controller.Get();

            var postResponseResult = obtained as OkNegotiatedContentResult<PostResponse>;

            Assert.AreEqual(_offset, postResponseResult.Content.Metadata.Offset);
        }
    }
}