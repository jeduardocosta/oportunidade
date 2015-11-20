using NUnit.Framework;
using MinutoSeguros.BlogFeed.API.Controllers;
using MinutoSeguros.BlogFeed.API.Models;
using MinutoSeguros.BlogFeed.API.Parsers;
using MinutoSeguros.BlogFeed.Core.Entities;
using MinutoSeguros.BlogFeed.Core.Exceptions;
using MinutoSeguros.BlogFeed.Core.Readers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Results;
using FluentAssertions;

namespace MinutoSeguros.BlogFeed.API.Tests.Controllers
{
    [TestFixture]
    public class TopWordsControllerTest
    {
        private TopWordsController _controller;

        private Mock<IRequestParametersParser> _mockRequestParametersParser;
        private Mock<IBlogFeedReader> _mockBlogFeedReader;

        private const string Limit = "10";
        private const string Offset = "0";
        private const string TopWords = "10";

        [SetUp]
        public void SetUp()
        {
            _mockRequestParametersParser = new Mock<IRequestParametersParser>();
            _mockBlogFeedReader = new Mock<IBlogFeedReader>();

            _mockRequestParametersParser
                .Setup(it => it.Parse(It.IsAny<HttpRequestMessage>()))
                .Returns(new RequestParameters("feedUrl", Limit, Offset, TopWords));

            _mockBlogFeedReader
                .Setup(it => it.Read(It.IsAny<string>()))
                .Returns(new List<BlogFeedContent>());

            _controller = new TopWordsController(_mockRequestParametersParser.Object, _mockBlogFeedReader.Object);
            _controller.Request = new HttpRequestMessage();
        }

        [Test]
        public void Should_ReturnOkNegotiatedContentResult_InTopWordsController()
        {
            _controller
                .Get()
                .Should()
                .BeOfType<OkNegotiatedContentResult<TopWordsResponse>>();

            _mockRequestParametersParser.Verify(it => it.Parse(It.IsAny<HttpRequestMessage>()), Times.Once);
            _mockBlogFeedReader.Verify(it => it.Read(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Should_CallDependencies_InTopWordsController()
        {
            _controller.Get();

            _mockRequestParametersParser.Verify(it => it.Parse(It.IsAny<HttpRequestMessage>()), Times.Once);
            _mockBlogFeedReader.Verify(it => it.Read(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void Should_ReturnInternalServerError_WhenCustomErrorExceptionIsThrow_InTopWordsController()
        {
            const string errorMessage = "sample error message";

            _mockRequestParametersParser
                .Setup(it => it.Parse(It.IsAny<HttpRequestMessage>()))
                .Throws(new CustomErrorException(errorMessage));

            _controller
                .Get()
                .Should()
                .BeOfType<ExceptionResult>();
        }

        [Test]
        public void Should_ReturnInternalServerError_WhenExceptionIsThrow_InTopWordsController()
        {
            _mockRequestParametersParser
                .Setup(it => it.Parse(It.IsAny<HttpRequestMessage>()))
                .Throws(new Exception());

            _controller
                .Get()
                .Should()
                .BeOfType<ExceptionResult>();
        }
    }
}