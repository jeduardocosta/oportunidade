using NUnit.Framework;
using MinutoSeguros.BlogFeed.API.Models;
using MinutoSeguros.BlogFeed.Core.Entities;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestFixture]
    public class PostResponseTest
    {
        [Test]
        public void Should_CreatePostResponseObject()
        {
            var requestParameters = new RequestParameters("feedUrl", "10", "0", "1");
            var blogFeedContent = GivenAnSetOfBlogFeedContent();

            new PostResponse(requestParameters, blogFeedContent)
                .Should()
                .NotBeNull();
        }

        private IEnumerable<BlogFeedContent> GivenAnSetOfBlogFeedContent()
        {
            return new List<BlogFeedContent>
            {
                new BlogFeedContent("title", DateTime.Now, new [] {"category"}, "content")
            };
        }
    }
}