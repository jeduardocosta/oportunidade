using NUnit.Framework;
using MinutoSeguros.BlogFeed.API.Models;
using MinutoSeguros.BlogFeed.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestFixture]
    public class TopWordsResponseTest
    {
        [Test]
        public void Should_CreateTopWordsResponseObject()
        {
            var requestParameters = GivenRequestParameters();
            var blogFeedContent = GivenBlogFeedContent();

            const int expectedSampleWordOccurrences = 8;

            var obtained = new TopWordsResponse(requestParameters, blogFeedContent);

            var obtainedSampleWordOccurrences = obtained.TopWords
                .Where(it => it.Name == "sample")
                .Select(it => it.Occurrences)
                .FirstOrDefault();

            obtainedSampleWordOccurrences.Should().Be(expectedSampleWordOccurrences);
        }

        private RequestParameters GivenRequestParameters()
        {
            return new RequestParameters("feedUrl", "10", "0", "1");
        }

        private IEnumerable<BlogFeedContent> GivenBlogFeedContent()
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