using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.Core.Helpers;
using MinutoSeguros.BlogFeed.Log;
using Moq;

namespace MinutoSeguros.BlogFeed.Core.Tests.Helpers
{
    [TestClass]
    public class HtmlHelperTest
    {
        private Mock<ILogger> _mockLogger;

        private IHtmlHelper _htmlHelper;

        [TestInitialize]
        public void Init()
        {
            _mockLogger = new Mock<ILogger>();

            _htmlHelper = new HtmlHelper(_mockLogger.Object);
        }

        [TestMethod]
        public void Should_RemoveHtmlTags_FromHtmlContent()
        {
            const string htmlContent = "<html><body>sample text</body></html>";
            const string expected = "sample text";

            var obtained = _htmlHelper.RemoveTags(htmlContent);

            Assert.AreEqual(expected, obtained);
        }

        [TestMethod]
        public void Should_RemoveHtmlTags_FromHtmlContent_AndReturnUncodedContent()
        {
            const string htmlContent = "<html>city of s&#227;o paulo</html>";
            const string expected = "city of são paulo";

            var obtained = _htmlHelper.RemoveTags(htmlContent);

            Assert.AreEqual(expected, obtained);
        }
    }
}