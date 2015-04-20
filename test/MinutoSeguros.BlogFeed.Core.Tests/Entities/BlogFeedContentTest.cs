using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinutoSeguros.BlogFeed.Core.Tests.Entities
{
    [TestClass]
    public class BlogFeedContentTest
    {
        [TestMethod]
        public void Should_CreateBloogFeedContentObject_AndCheckTitleParameter()
        {
            const string title = "sample title";

            var obtained = new BlogFeedContent(title, It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>(), It.IsAny<string>());

            Assert.AreEqual(title, obtained.Title);
        }

        [TestMethod]
        public void Should_CreateBloogFeedContentObject_AndCheckPublishDateParameter()
        {
            var publishDate = DateTime.Now;

            var obtained = new BlogFeedContent(It.IsAny<string>(), publishDate, It.IsAny<IEnumerable<string>>(), It.IsAny<string>());

            Assert.AreEqual(publishDate, obtained.PublishDate);
        }

        [TestMethod]
        public void Should_CreateBloogFeedContentObject_AndCheckCategoriesParameter()
        {
            var categories = new[] { "category 1", "category 2" };

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), categories, It.IsAny<string>());

            Assert.IsTrue(categories.SequenceEqual(obtained.Categories));
        }

        [TestMethod]
        public void Should_CreateBloogFeedContentObject_AndCheckContentParameter()
        {
            const string content = "sample content";

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>(), content);

            Assert.AreEqual(content, obtained.FullContent);
        }

        [TestMethod]
        public void Should_GetTopWordsValue_WhenCreatingBloogFeedContentObject()
        {
            const string content = "sample html content";

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), new[] { It.IsAny<string>() }, content);

            var expectedTopWords = new Dictionary<string, int>
            {
                {"sample", 1},
                {"html", 1},
                {"content", 1}
            };

            Assert.IsTrue(expectedTopWords.SequenceEqual(obtained.TopWords));
        }

        [TestMethod]
        public void Should_GetTopWordsValue_AndRemoveArticles_WhenCreatingBloogFeedContentObject()
        {
            const string htmlContentWithArticles = "o sample uma um os as sample";

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), new[] { It.IsAny<string>() }, htmlContentWithArticles);

            var expectedTopWords = new Dictionary<string, int> { {"sample", 2} };

            Assert.IsTrue(expectedTopWords.SequenceEqual(obtained.TopWords));
        }

        [TestMethod]
        public void Should_GetTopWordsValue_AndRemovePrepositions_WhenCreatingBloogFeedContentObject()
        {
            const string htmlContentWithPrepositions = "desde para test por malgrado exceto após sample sample entre em sem sob trás sample";

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), new[] { It.IsAny<string>() }, htmlContentWithPrepositions);

            var expectedTopWords = new Dictionary<string, int> { { "sample", 3 }, { "test", 1 } };

            Assert.IsTrue(expectedTopWords.SequenceEqual(obtained.TopWords));
        }

        [TestMethod]
        public void Should_GetTopWordsValue_FromTextRange_WhenCreatingBloogFeedContentObject()
        {
            const int length = 1000;
            const string sampleWord = "sampleword";

            var htmlContentFromTextRange = string.Concat(Enumerable.Repeat(string.Concat(sampleWord, " "), length));

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), new[] { It.IsAny<string>() }, htmlContentFromTextRange);

            var expectedTopWords = new Dictionary<string, int> { { "sampleword", length } };

            Assert.IsTrue(expectedTopWords.SequenceEqual(obtained.TopWords));
        }
    }
}