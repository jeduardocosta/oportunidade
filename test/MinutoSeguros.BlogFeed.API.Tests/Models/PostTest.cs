using NUnit.Framework;
using MinutoSeguros.BlogFeed.API.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestFixture]
    public class PostTest
    {
        [Test]
        public void Should_CreatePostsObject_AndCheckTitleParameter()
        {
            const string title = "sample-title";

            var obtained = new Post(title, It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>(), new Dictionary<string, int>(), It.IsAny<int>());

            obtained.Title.Should().Be(title);
        }

        [Test]
        public void Should_CreatePostsObject_AndCheckPublishDateParameter()
        {
            var publishDate = DateTime.Now;

            var obtained = new Post(It.IsAny<string>(), publishDate, It.IsAny<IEnumerable<string>>(), new Dictionary<string, int>(), It.IsAny<int>());

            obtained.PublishDate.Should().Be(publishDate);
        }

        [Test]
        public void Should_CreatePostsObject_AndCheckCategoriesParameter()
        {
            var categories = new[] { "category 1", "category 2" };

            var obtained = new Post(It.IsAny<string>(), It.IsAny<DateTime>(), categories, new Dictionary<string, int>(), It.IsAny<int>());

            obtained.Categories.ShouldAllBeEquivalentTo(categories);
        }

        [Test]
        public void Should_CreatePostsObject_AndCheckSourceTopWordsParameter()
        {
            var sourceTopWords = GivenSourceTopWords();

            var obtained = new Post(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>(), sourceTopWords, It.IsAny<int>());

            obtained.TotalWords.Should().Be(sourceTopWords.Count);
        }

        [Test]
        public void Should_CreatePostsObject_AndCheckNumberOfTopWordsParameter()
        {
            const int numberOfTopWords = 2;

            var entry = GivenSourceTopWords();
            var expected = new List<TopWord> { new TopWord("test", 21), new TopWord("word", 17) };

            var obtained = new Post(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>(), entry, numberOfTopWords);

            obtained.TopWords.ShouldAllBeEquivalentTo(expected);
        }

        private static Dictionary<string, int> GivenSourceTopWords()
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