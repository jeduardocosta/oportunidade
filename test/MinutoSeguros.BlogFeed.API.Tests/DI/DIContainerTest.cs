using System;
using FluentAssertions;
using NUnit.Framework;
using MinutoSeguros.BlogFeed.API.DI;
using MinutoSeguros.BlogFeed.API.Parsers;
using MinutoSeguros.BlogFeed.Core.Helpers;
using MinutoSeguros.BlogFeed.Core.Parsers;
using MinutoSeguros.BlogFeed.Core.Readers;
using MinutoSeguros.BlogFeed.Log;

namespace MinutoSeguros.BlogFeed.API.Tests.DI
{
    [TestFixture]
    public class DIContainerTest
    {
        [TestCase(typeof(IRequestParametersParser), typeof(RequestParametersParser))]
        [TestCase(typeof(IBlogFeedReader), typeof(BlogFeedReader))]
        [TestCase(typeof(IBlogFeedContentParser), typeof(BlogFeedContentParser))]
        [TestCase(typeof(IHtmlHelper), typeof(HtmlHelper))]
        [TestCase(typeof(IUrlHelper), typeof(UrlHelper))]
        [TestCase(typeof(ILogger), typeof(LoggerWrapper))]
        public void Should_ResolveDependencies_UsingDIContainer(Type entry, Type expected)
        {
            DIContainer
                .Resolve(entry)
                .Should()
                .BeOfType(expected);
        }
    }
}