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
    public class TopWordTest
    {
        [TestMethod]
        public void ShouldCreateTopWordObject()
        {
            const string name = "sample name";
            const int occurrences = 15;

            var obtained = new TopWord(name, occurrences);

            Assert.AreEqual(name, obtained.Name);
            Assert.AreEqual(occurrences, obtained.Occurrences);
        }
    }
}