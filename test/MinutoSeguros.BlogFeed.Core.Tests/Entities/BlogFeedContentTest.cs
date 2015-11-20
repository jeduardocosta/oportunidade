using NUnit.Framework;
using MinutoSeguros.BlogFeed.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace MinutoSeguros.BlogFeed.Core.Tests.Entities
{
    [TestFixture]
    public class BlogFeedContentTest
    {
        [Test]
        public void Should_CreateBloogFeedContentObject_AndCheckTitleParameter()
        {
            const string title = "sample title";

            var obtained = new BlogFeedContent(title, It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>(), It.IsAny<string>());

            obtained.Title.Should().Be(title);
        }

        [Test]
        public void Should_CreateBloogFeedContentObject_AndCheckPublishDateParameter()
        {
            var publishDate = DateTime.Now;

            var obtained = new BlogFeedContent(It.IsAny<string>(), publishDate, It.IsAny<IEnumerable<string>>(), It.IsAny<string>());

            obtained.PublishDate.Should().Be(publishDate);
        }

        [Test]
        public void Should_CreateBloogFeedContentObject_AndCheckCategoriesParameter()
        {
            var categories = new[] { "category 1", "category 2" };

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), categories, It.IsAny<string>());

            obtained.Categories.ShouldAllBeEquivalentTo(categories);
        }

        [Test]
        public void Should_CreateBloogFeedContentObject_AndCheckContentParameter()
        {
            const string content = "sample content";

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<IEnumerable<string>>(), content);

            obtained.FullContent.Should().Be(content);
        }

        [Test]
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

            obtained.TopWords.ShouldAllBeEquivalentTo(expectedTopWords);
        }

        [Test]
        public void Should_GetTopWordsValue_AndRemoveArticles_WhenCreatingBloogFeedContentObject()
        {
            const string htmlContentWithArticles = "o sample uma um os as sample";

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), new[] { It.IsAny<string>() }, htmlContentWithArticles);

            var expectedTopWords = new Dictionary<string, int> { {"sample", 2} };

            obtained.TopWords.ShouldAllBeEquivalentTo(expectedTopWords);
        }

        [Test]
        public void Should_GetTopWordsValue_AndRemovePrepositions_WhenCreatingBloogFeedContentObject()
        {
            const string htmlContentWithPrepositions = "desde para test por malgrado exceto após sample sample entre em sem sob trás sample";

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), new[] { It.IsAny<string>() }, htmlContentWithPrepositions);

            var expectedTopWords = new Dictionary<string, int> { { "sample", 3 }, { "test", 1 } };

            obtained.TopWords.ShouldAllBeEquivalentTo(expectedTopWords);
        }

        [Test]
        public void Should_GetTopWordsValue_FromTextRange_WhenCreatingBloogFeedContentObject()
        {
            const int length = 1000;
            const string sampleWord = "sampleword";

            var htmlContentFromTextRange = string.Concat(Enumerable.Repeat(string.Concat(sampleWord, " "), length));

            var obtained = new BlogFeedContent(It.IsAny<string>(), It.IsAny<DateTime>(), new[] { It.IsAny<string>() }, htmlContentFromTextRange);

            var expectedTopWords = new Dictionary<string, int> { { "sampleword", length } };

            obtained.TopWords.ShouldAllBeEquivalentTo(expectedTopWords);
        }

        [Test]
        public void Should_RemoveAllArticles_FromText()
        {
            const string text = "o a os as um uma uns umas";

            BlogFeedContent
                .RemoveArticles(text)
                .Should()
                .BeEmpty();
        }

        [Test]
        public void Should_RemoveAllUppercaseArticles_FromText()
        {
            var text = "o a os as um uma uns umas".ToUpper();

            BlogFeedContent
                .RemoveArticles(text)
                .Should()
                .BeEmpty();
        }

        [Test]
        public void Should_RemoveAllPrepositions_FromText()
        {
            const string text = "a ante após até com contra de desde em entre para por perante segundo sem sob sobre trás afora fora exceto salvo malgrado durante mediante segundo menos";

            BlogFeedContent
                .RemovePrepositions(text)
                .Should()
                .BeEmpty();
        }

        [Test]
        public void Should_RemoveAllUppercasePrepositions_FromText()
        {
            var text = "a ante após até com contra de desde em entre para por perante segundo sem sob sobre trás afora fora exceto salvo malgrado durante mediante segundo menos"
                .ToUpper();

            BlogFeedContent
                .RemovePrepositions(text)
                .Should()
                .BeEmpty();
        }
    }
}