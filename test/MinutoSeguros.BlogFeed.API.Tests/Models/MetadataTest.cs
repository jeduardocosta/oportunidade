using FluentAssertions;
using NUnit.Framework;
using MinutoSeguros.BlogFeed.API.Models;
using Moq;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestFixture]
    public class MetadataTest
    {
        [Test]
        public void Should_CreateMetadataObject_AndCheckLimitParameter()
        {
            new Metadata(5, It.IsAny<int>())
                .Limit
                .Should()
                .Be(5);
        }

        [Test]
        public void Should_CreateMetadataObject_AndCheckOffsetParameter()
        {
            new Metadata(It.IsAny<int>(), 3)
                .Offset
                .Should()
                .Be(3);
        }
    }
}