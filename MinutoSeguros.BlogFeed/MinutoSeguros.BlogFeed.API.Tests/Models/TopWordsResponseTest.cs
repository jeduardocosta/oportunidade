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
        public void ShouldCreateTopWordsResponseObject()
        {
            var requestParameters = new RequestParameters("feedUrl", "10", "0", "1");
            var blogFeedContent = new List<BlogFeedContent>
            {
                new BlogFeedContent("title", DateTime.Now, new [] {"category"}, "sample test"),
                new BlogFeedContent("title", DateTime.Now, new [] {"category"}, "SAMPLE word sample test sample sample"),
                new BlogFeedContent("title", DateTime.Now, new [] {"category"}, "sample sample date samPLE")
            };

            var obtained = new TopWordsResponse(requestParameters, blogFeedContent);

            const int expectedSampleWordOccurrences = 8;

            var obtainedSampleWordOccurrences = obtained.TopWords
                .Where(it => it.Name == "sample")
                .Select(it => it.Occurrences)
                .FirstOrDefault();

            Assert.IsNotNull(obtained);
            Assert.AreEqual(expectedSampleWordOccurrences, obtainedSampleWordOccurrences);
        }
    }
}