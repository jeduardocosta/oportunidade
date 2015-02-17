using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Controllers;
using MinutoSeguros.BlogFeed.API.DI;
using MinutoSeguros.BlogFeed.API.Models;
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
    public class PostsControllerIntegrationTest
    {
        private PostsController _controller;

        private const string FeedUrl = "http://www.minutoseguros.com.br/blog/feed/";

        [TestInitialize]
        public void Init()
        {
            _controller = DIContainer.Resolve<PostsController>();
            _controller.Request = new HttpRequestMessage();
        }

        [TestMethod]
        public void ShouldGetPostsInPostsControllerWithIntegration()
        {
            _controller.Request.RequestUri = new Uri(string.Format("http://localhost.com/posts?feedurl={0}", FeedUrl));

            var obtained = _controller.Get();

            var postResponseResult = obtained as OkNegotiatedContentResult<PostResponse>;

            Assert.IsTrue(postResponseResult.Content.Metadata.Limit > 0);
            Assert.IsTrue(postResponseResult.Content.Posts.Any());
        }

        [TestMethod]
        public void ShouldGetPostsWithPaginationParametersInPostsControllerWithIntegration()
        {
            const int limit = 5;
            const int offset = 1;

            _controller.Request.RequestUri = new Uri(string.Format("http://localhost.com/posts?feedurl={0}&limit={1}&offset={2}", FeedUrl, limit, offset));

            var obtained = _controller.Get();

            var postResponseResult = obtained as OkNegotiatedContentResult<PostResponse>;

            Assert.AreEqual(limit, postResponseResult.Content.Metadata.Limit);
            Assert.AreEqual(offset, postResponseResult.Content.Metadata.Offset);
            Assert.AreEqual(limit, postResponseResult.Content.Posts.Count());
        }
    }
}