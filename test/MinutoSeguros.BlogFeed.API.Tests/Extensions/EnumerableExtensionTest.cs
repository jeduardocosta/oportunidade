using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MinutoSeguros.BlogFeed.API.Extensions;

namespace MinutoSeguros.BlogFeed.API.Tests.Extensions
{
    [TestFixture]
    public class EnumerableExtensionTest
    {
        [Test]
        public void Should_DoPagination_InListStructure()
        {
            const int limit = 3;
            const int offset = 1;

            Enumerable.Range(1, 10)
                .Pagination(limit, offset)
                .ShouldAllBeEquivalentTo(new List<int> { 4, 5, 6 });
        }
    }
}