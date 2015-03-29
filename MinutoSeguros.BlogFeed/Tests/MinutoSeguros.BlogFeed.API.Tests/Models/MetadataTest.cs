using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace MinutoSeguros.BlogFeed.API.Tests.Models
{
    [TestClass]
    public class MetadataTest
    {
        [TestMethod]
        public void Should_CreateMetadataObject_AndCheckLimitParameter()
        {
            const int limit = 5;

            var obtained = new Metadata(limit, It.IsAny<int>());

            Assert.AreEqual(limit, obtained.Limit);
        }

        [TestMethod]
        public void Should_CreateMetadataObject_AndCheckOffsetParameter()
        {
            const int offset = 3;

            var obtained = new Metadata(It.IsAny<int>(), offset);

            Assert.AreEqual(offset, obtained.Offset);
        }
    }
}