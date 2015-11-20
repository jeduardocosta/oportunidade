using NUnit.Framework;
using MinutoSeguros.BlogFeed.API.Models;
using Moq;
using FluentAssertions;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestFixture]
    public class TopWordTest
    {
        [Test]
        public void Should_CreateTopWordObject_AndCheckNameParameter()
        {
            const string name = "sample name";

            var obtained = new TopWord(name, It.IsAny<int>());

            obtained.Name.Should().Be(name);
        }

        [Test]
        public void Should_CreateTopWordObject_AndCheckOccurrencesParameter()
        {
            const int occurrences = 15;

            var obtained = new TopWord(It.IsAny<string>(), occurrences);

            obtained.Occurrences.Should().Be(occurrences);
        }
    }
}