using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Results;
using FluentAssertions;
using MinutoSeguros.BlogFeed.API.Controllers;
using MinutoSeguros.BlogFeed.API.DI;
using MinutoSeguros.BlogFeed.API.Models;
using NUnit.Framework;

namespace MinutoSeguros.BlogFeed.API.IntegrationTests.Controllers
{
    [TestFixture]
    public class PostsControllerIntegrationTest
    {
        private PostsController _controller;

        private const string FeedUrl = "http://www.minutoseguros.com.br/blog/feed/";

        [SetUp]
        public void SetUp()
        {
            _controller = DIContainer.Resolve<PostsController>();
            _controller.Request = new HttpRequestMessage();
        }

        [Test]
        public void Should_GetData_FromPostsController()
        {
            _controller.Request.RequestUri = new Uri($"http://localhost.com/posts?feedurl={FeedUrl}");

            var obtained = _controller.Get();

            (obtained as OkNegotiatedContentResult<PostResponse>)
                .Content
                .Posts
                .Any()
                .Should()
                .BeTrue();
        }

        [Test]
        public void Should_GetMetadataLimitValue_FromPostsController()
        {
            _controller.Request.RequestUri = new Uri($"http://localhost.com/posts?feedurl={FeedUrl}&limit=3&offset=1");

            var obtained = _controller.Get();

            (obtained as OkNegotiatedContentResult<PostResponse>)
                .Content
                .Metadata
                .Limit
                .Should()
                .Be(3);
        }

        [Test]
        public void Should_GetMetadataOffsetValue_FromPostsController()
        {
            _controller.Request.RequestUri = new Uri($"http://localhost.com/posts?feedurl={FeedUrl}&limit=5&offset=2");

            var obtained = _controller.Get();

            (obtained as OkNegotiatedContentResult<PostResponse>)
                .Content
                .Metadata
                .Offset
                .Should()
                .Be(2);
        }
    }
}