using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Models;
using Moq;
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
        public void Should_CreateTopWordObject_AndCheckNameParameter()
        {
            const string name = "sample name";

            var obtained = new TopWord(name, It.IsAny<int>());

            Assert.AreEqual(name, obtained.Name);
        }

        [TestMethod]
        public void Should_CreateTopWordObject_AndCheckOccurrencesParameter()
        {
            const int occurrences = 15;

            var obtained = new TopWord(It.IsAny<string>(), occurrences);

            Assert.AreEqual(occurrences, obtained.Occurrences);
        }
    }
}