using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Models;
using MinutoSeguros.BlogFeed.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestClass]
    public class PostResponseTest
    {
        [TestMethod]
        public void ShouldCreatePostResponseObject()
        {
            var requestParameters = new RequestParameters("feedUrl", "10", "0", "1");
            var blogFeedContent = GivenAnSetOfBlogFeedContent();

            var obtained = new PostResponse(requestParameters, blogFeedContent);

            Assert.IsNotNull(obtained);
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