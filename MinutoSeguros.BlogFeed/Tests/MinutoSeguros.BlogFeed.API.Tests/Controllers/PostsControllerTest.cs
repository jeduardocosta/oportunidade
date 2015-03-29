using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Controllers;
using MinutoSeguros.BlogFeed.API.Parsers;
using MinutoSeguros.BlogFeed.Core.Readers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using MinutoSeguros.BlogFeed.Core.Exceptions;
using MinutoSeguros.BlogFeed.API.Models;
using MinutoSeguros.BlogFeed.Core.Entities;
using System.Web.Http.Results;

namespace MinutoSeguros.BlogFeed.API.Tests.Controllers
{
    [TestClass]
    public class PostsControllerTest
    {
        private PostsController _controller;

        private Mock<IRequestParametersParser> _mockRequestParametersParser;
        private Mock<IBlogFeedReader> _mockBlogFeedReader;

        private const string Limit = "10";
        private const string Offset = "0";
        private const string TopWords = "10";

        [TestInitialize]
        public void Init()
        {
            _mockRequestParametersParser = new Mock<IRequestParametersParser>();
            _mockBlogFeedReader = new Mock<IBlogFeedReader>();

            _mockRequestParametersParser
                .Setup(it => it.Parse(It.IsAny<HttpRequestMessage>()))
                .Returns(new RequestParameters("feedUrl", Limit, Offset, TopWords));

            _mockBlogFeedReader
                .Setup(it => it.Read(It.IsAny<string>()))
                .Returns(new List<BlogFeedContent>());

            _controller = new PostsController(_mockRequestParametersParser.Object, _mockBlogFeedReader.Object);
            _controller.Request = new HttpRequestMessage();
        }

        [TestMethod]
        public void Should_GetPosts_InPostsController()
        {
            var obtained = _controller.Get();

            Assert.IsInstanceOfType(obtained, typeof(OkNegotiatedContentResult<PostResponse>));

            _mockRequestParametersParser.Verify(it => it.Parse(It.IsAny<HttpRequestMessage>()), Times.Once);
            _mockBlogFeedReader.Verify(it => it.Read(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Should_ReturnInternalServerError_WhenCustomErrorExceptionIsThrow_InPostsController()
        {
            const string errorMessage = "sample error message";

            _mockRequestParametersParser
                .Setup(it => it.Parse(It.IsAny<HttpRequestMessage>()))
                .Throws(new CustomErrorException(errorMessage));

            var obtained = _controller.Get();
            
            Assert.IsInstanceOfType(obtained, typeof(ExceptionResult));
        }

        [TestMethod]
        public void Should_ReturnInternalServerError_WhenExceptionIsThrow_InPostsController()
        {
            _mockRequestParametersParser
                .Setup(it => it.Parse(It.IsAny<HttpRequestMessage>()))
                .Throws(new Exception());

            var obtained = _controller.Get();

            Assert.IsInstanceOfType(obtained, typeof(ExceptionResult));
        }
    }
}