using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestClass]
    public class PostTest
    {
        [TestMethod]
        public void Should_CreatePostsObject_AndCheckTitleParameter()
        {
            const string title = "sample-title";

            var obtained = new Post(title, It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>(), new Dictionary<string, int>(), It.IsAny<int>());

            Assert.AreEqual(title, obtained.Title);
        }

        [TestMethod]
        public void Should_CreatePostsObject_AndCheckPublishDateParameter()
        {
            var publishDate = DateTime.Now;

            var obtained = new Post(It.IsAny<string>(), publishDate, It.IsAny<IEnumerable<string>>(), new Dictionary<string, int>(), It.IsAny<int>());

            Assert.AreEqual(publishDate, obtained.PublishDate);
        }

        [TestMethod]
        public void Should_CreatePostsObject_AndCheckCategoriesParameter()
        {
            var categories = new[] { "category 1", "category 2" };

            var obtained = new Post(It.IsAny<string>(), It.IsAny<DateTime>(), categories, new Dictionary<string, int>(), It.IsAny<int>());

            Assert.IsTrue(categories.SequenceEqual(obtained.Categories));
        }

        [TestMethod]
        public void Should_CreatePostsObject_AndCheckSourceTopWordsParameter()
        {
            var sourceTopWords = GivenASourceTopWords();

            var obtained = new Post(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>(), sourceTopWords, It.IsAny<int>());

            Assert.AreEqual(sourceTopWords.Count, obtained.TotalWords);
        }

        [TestMethod]
        public void Should_CreatePostsObject_AndCheckNumberOfTopWordsParameter()
        {
            const int numberOfTopWords = 2;

            var sourceTopWords = GivenASourceTopWords();
            var expectedTopWords = new List<TopWord> { new TopWord("test", 21), new TopWord("word", 17) };

            var obtained = new Post(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>(), sourceTopWords, numberOfTopWords);

            Assert.IsTrue(expectedTopWords.SequenceEqual(obtained.TopWords));
        }

        private static Dictionary<string, int> GivenASourceTopWords()
        {
            return new Dictionary<string, int>
            {
                {"something", 7},
                {"test", 21},
                {"sample", 4},
                {"method", 5},
                {"class", 6},
                {"object", 2},
                {"word", 17}
            };
        }
    }
}