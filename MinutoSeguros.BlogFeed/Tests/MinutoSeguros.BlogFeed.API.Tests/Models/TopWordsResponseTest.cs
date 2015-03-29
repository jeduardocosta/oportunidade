using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Models;
using MinutoSeguros.BlogFeed.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestClass]
    public class TopWordsResponseTest
    {
        [TestMethod]
        public void Should_CreateTopWordsResponseObject()
        {
            var requestParameters = GivenARequestParameters();
            var blogFeedContent = GivenABlogFeedContent();

            const int expectedSampleWordOccurrences = 8;

            var obtained = new TopWordsResponse(requestParameters, blogFeedContent);

            var obtainedSampleWordOccurrences = obtained.TopWords
                .Where(it => it.Name == "sample")
                .Select(it => it.Occurrences)
                .FirstOrDefault();

            Assert.AreEqual(expectedSampleWordOccurrences, obtainedSampleWordOccurrences);
        }

        private RequestParameters GivenARequestParameters()
        {
            return new RequestParameters("feedUrl", "10", "0", "1");
        }

        private IEnumerable<BlogFeedContent> GivenABlogFeedContent()
        {
            return new List<BlogFeedContent>
            {
                new BlogFeedContent("title", DateTime.Now, new [] {"category"}, "sample test"),
                new BlogFeedContent("title", DateTime.Now, new [] {"category"}, "SAMPLE word sample test sample sample"),
                new BlogFeedContent("title", DateTime.Now, new [] {"category"}, "sample sample date samPLE")
            };
        }
    }
}