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
    public class TopWordsControllerIntegrationTest
    {
        private TopWordsController _controller;

        private const string FeedUrl = "http://www.minutoseguros.com.br/blog/feed/";

        [SetUp]
        public void SetUp()
        {
            _controller = DIContainer.Resolve<TopWordsController>();
            _controller.Request = new HttpRequestMessage();
        }

        [Test]
        public void Should_GetData_FromTopWordsController()
        {
            _controller.Request.RequestUri = new Uri($"http://localhost.com/topWords?feedurl={FeedUrl}");

            var obtained = _controller.Get();

            (obtained as OkNegotiatedContentResult<TopWordsResponse>)
                .Content
                .TopWords
                .Any()
                .Should()
                .BeTrue();
        }

        [Test]
        public void Should_GetMetadataLimitValue_FromTopWordsController()
        {
            _controller.Request.RequestUri = new Uri($"http://localhost.com/topWords?feedurl={FeedUrl}&limit={4}&offset={2}");

            var obtained = _controller.Get();

            (obtained as OkNegotiatedContentResult<TopWordsResponse>)
                .Content
                .Metadata
                .Limit
                .Should()
                .Be(4);
        
        }

        [Test]
        public void Should_GetMetadataOffsetValue_FromTopWordsController()
        {
            _controller.Request.RequestUri = new Uri($"http://localhost.com/topWords?feedurl={FeedUrl}&limit={4}&offset={5}");

            var obtained = _controller.Get();

            (obtained as OkNegotiatedContentResult<TopWordsResponse>)
                .Content
                .Metadata
                .Offset
                .Should()
                .Be(5);
        }
    }
}