using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinutoSeguros.BlogFeed.API.Extensions;

namespace MinutoSeguros.BlogFeed.API.Tests.Extensions
{
    [TestClass]
    public class EnumerableExtensionTest
    {
        [TestMethod]
        public void ShouldDoPaginationInListStructure()
        {
            const int limit = 3;
            const int offset = 1;

            var records = Enumerable.Range(1, 10);

            var expected = new List<int> { 4, 5, 6 };

            var obtained = records.Pagination(limit, offset);

            Assert.IsTrue(expected.SequenceEqual(obtained));
        }
    }
}