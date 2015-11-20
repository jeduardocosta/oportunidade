using NUnit.Framework;
using MinutoSeguros.BlogFeed.Core.Helpers;
using FluentAssertions;

namespace MinutoSeguros.BlogFeed.Core.Tests.Helpers
{
    [TestFixture]
    public class UrlHelperTest
    {
        private IUrlHelper _urlHelper;

        [SetUp]
        public void SetUp()
        {
            _urlHelper = new UrlHelper();
        }

        [Test]
        public void Should_ReturnTrue_ToAValidUrl()
        {
            const string validBlogFeedUrl = "http://www.minutoseguros.com.br/blog/feed/";

            _urlHelper
                .IsValidUrl(validBlogFeedUrl)
                .Should()
                .BeTrue();
        }

        [Test]
        public void Should_ReturnTrue_ToAValidRelativeUrl()
        {
            const string invalidBlogFeedUrl = "blogfeedurl/sample";

            _urlHelper
                .IsValidUrl(invalidBlogFeedUrl)
                .Should()
                .BeTrue();
        }

        [Test]
        public void Should_ReturnFalse_ToAInvalidAbsoluteUrl()
        {
            const string invalidBlogFeedUrl = "blogfeedurl/sample";

            _urlHelper
                .IsValidAbsoluteUrl(invalidBlogFeedUrl)
                .Should()
                .BeFalse();
        }
    }
}