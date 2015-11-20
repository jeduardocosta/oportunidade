using FluentAssertions;
using NUnit.Framework;
using MinutoSeguros.BlogFeed.API.Controllers;
using MinutoSeguros.BlogFeed.API.DI;

namespace MinutoSeguros.BlogFeed.API.Tests.DI
{
    [TestFixture]
    public class DIContainerTest
    {
        [Test]
        public void Should_ResolveDependencies_UsingDIContainer()
        {
            DIContainer
                .Resolve<PostsController>()
                .Should()
                .NotBeNull();
        }
    }
}