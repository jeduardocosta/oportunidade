using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.Core.Tests.Helpers
{
    [TestClass]
    public class UrlHelperTest
    {
        private IUrlHelper _urlHelper;

        [TestInitialize]
        public void Init()
        {
            _urlHelper = new UrlHelper();
        }

        [TestMethod]
        public void Should_ReturnTrue_ToAValidUrl()
        {
            const string validBlogFeedUrl = "http://www.minutoseguros.com.br/blog/feed/";

            var obtained = _urlHelper.IsValidUrl(validBlogFeedUrl);

            Assert.IsTrue(obtained);
        }

        [TestMethod]
        public void Should_ReturnTrue_ToAValidRelativeUrl()
        {
            const string invalidBlogFeedUrl = "blogfeedurl/sample";

            var obtained = _urlHelper.IsValidUrl(invalidBlogFeedUrl);

            Assert.IsTrue(obtained);
        }

        [TestMethod]
        public void Should_ReturnFalse_ToAInvalidAbsoluteUrl()
        {
            const string invalidBlogFeedUrl = "blogfeedurl/sample";

            var obtained = _urlHelper.IsValidAbsoluteUrl(invalidBlogFeedUrl);

            Assert.IsFalse(obtained);
        }
    }
}