using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestClass]
    public class PostTest
    {
        [TestMethod]
        public void ShouldCreatePostsObject()
        {
            const int numberOfTopWords = 2;
            
            const string title = "sample-title";
            var publishDate = DateTime.Now;
            var categories = new[] { "category 1", "category 2" };
            var sourceTopWords = new Dictionary<string, int>
            {
                {"something", 7},
                {"test", 21},
                {"sample", 4},
                {"method", 5},
                {"class", 6},
                {"object", 2},
                {"word", 17}
            };

            var expectedTopWords = new List<TopWord> { new TopWord("test", 21), new TopWord("word", 17) };

            var obtained = new Post(title, publishDate, categories, sourceTopWords, numberOfTopWords);

            Assert.AreEqual(title, obtained.Title);
            Assert.AreEqual(publishDate, obtained.PublishDate);
            Assert.AreEqual(sourceTopWords.Count, obtained.TotalWords);
            Assert.IsTrue(categories.SequenceEqual(obtained.Categories));
            Assert.IsTrue(expectedTopWords.SequenceEqual(obtained.TopWords));
        }
    }
}