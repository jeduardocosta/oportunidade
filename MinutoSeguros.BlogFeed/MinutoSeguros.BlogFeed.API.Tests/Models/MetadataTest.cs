using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestClass]
    public class MetadataTest
    {
        [TestMethod]
        public void ShouldCreateMetadataObject()
        {
            const int limit = 5;
            const int offset = 3;

            var obtained = new Metadata(limit, offset);

            Assert.AreEqual(limit, obtained.Limit);
            Assert.AreEqual(offset, obtained.Offset);
        }
    }
}