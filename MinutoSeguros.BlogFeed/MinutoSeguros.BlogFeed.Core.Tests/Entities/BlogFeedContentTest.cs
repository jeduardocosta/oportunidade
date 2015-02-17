using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.Core.Tests.Entities
{
    [TestClass]
    public class BlogFeedContentTest
    {
        private const string SampleTitle = "sample title";
        private DateTime SamplePublishDate = DateTime.Now;
        private IEnumerable<string> SampleCategories = new[] { "category 1", "category 2" };
        private string SampleHtmlContent = "sample html content";

        [TestMethod]
        public void ShouldCreateBloogFeedContentObject()
        {
            var obtained = new BlogFeedContent(SampleTitle, SamplePublishDate, SampleCategories, SampleHtmlContent);

            Assert.AreEqual(SampleTitle, obtained.Title);
            Assert.AreEqual(SamplePublishDate, obtained.PublishDate);
            Assert.IsTrue(SampleCategories.SequenceEqual(obtained.Categories));
            Assert.AreEqual(SampleHtmlContent, obtained.FullContent);
        }

        [TestMethod]
        public void ShouldGetTopWordsValueWhenCreatingBloogFeedContentObject()
        {
            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), new[] {It.IsAny<string>()}, SampleHtmlContent);

            var expectedTopWords = new Dictionary<string, int>
            {
                {"sample", 1},
                {"html", 1},
                {"content", 1}
            };

            Assert.IsTrue(expectedTopWords.SequenceEqual(obtained.TopWords));
        }

        [TestMethod]
        public void ShouldGetTopWordsValueAndRemoveArticlesWhenCreatingBloogFeedContentObject()
        {
            const string htmlContentWithArticles = "o sample uma um os as sample";

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), new[] { It.IsAny<string>() }, htmlContentWithArticles);

            var expectedTopWords = new Dictionary<string, int> { {"sample", 2} };

            Assert.IsTrue(expectedTopWords.SequenceEqual(obtained.TopWords));
        }

        [TestMethod]
        public void ShouldGetTopWordsValueAndRemovePrepositionsWhenCreatingBloogFeedContentObject()
        {
            const string htmlContentWithPrepositions = "desde para test por malgrado exceto após sample sample entre em sem sob trás sample";

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), new[] { It.IsAny<string>() }, htmlContentWithPrepositions);

            var expectedTopWords = new Dictionary<string, int> { { "sample", 3 }, { "test", 1 } };

            Assert.IsTrue(expectedTopWords.SequenceEqual(obtained.TopWords));
        }

        [TestMethod]
        public void ShouldGetTopWordsValueFromTextRangeWhenCreatingBloogFeedContentObject()
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