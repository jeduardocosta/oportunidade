using FluentAssertions;
using NUnit.Framework;
using MinutoSeguros.BlogFeed.Core.Helpers;
using MinutoSeguros.BlogFeed.Log;
using Moq;

namespace MinutoSeguros.BlogFeed.Core.Tests.Helpers
{
    [TestFixture]
    public class HtmlHelperTest
    {
        private Mock<ILogger> _mockLogger;

        private IHtmlHelper _htmlHelper;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();

            _htmlHelper = new HtmlHelper(_mockLogger.Object);
        }

        [Test]
        public void Should_RemoveHtmlTags_FromHtmlContent()
        {
            const string htmlContent = "<html><body>sample text</body></html>";
            const string expected = "sample text";

            _htmlHelper
                .RemoveTags(htmlContent)
                .Should()
                .Be(expected);
        }

        [Test]
        public void Should_RemoveHtmlTags_FromHtmlContent_AndReturnUncodedContent()
        {
            const string htmlContent = "<html>city of s&#227;o paulo</html>";
            const string expected = "city of são paulo";

            _htmlHelper
                .RemoveTags(htmlContent)
                .Should()
                .Be(expected);
        }
    }
}