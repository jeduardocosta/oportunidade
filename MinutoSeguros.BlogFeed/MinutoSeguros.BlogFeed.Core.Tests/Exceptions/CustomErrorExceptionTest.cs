using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.Core.Tests.Exceptions
{
    [TestClass]
    public class CustomErrorExceptionTest
    {
        [TestMethod]
        public void ShouldReturnTheEntryMessageInCustomErrorException()
        {
            const string expected = "sample error message";
            const string entry = "sample error message";

            try
            {
                throw new CustomErrorException(entry);
            }
            catch (CustomErrorException exception)
            {
                Assert.AreEqual(expected, exception.Message);
            }
        }
    }
}