using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.Core.Exceptions;

namespace MinutoSeguros.BlogFeed.Core.Tests.Exceptions
{
    [TestClass]
    public class InternalServerErrorExceptionTest
    {
        [TestMethod]
        public void ShouldReturnAnExactErrorMessageInInternalServerErrorException()
        {
            const string expected = "Internal server error.";

            try
            {
                throw new InternalServerErrorException();
            }
            catch (InternalServerErrorException exception)
            {
                Assert.IsTrue(string.Equals(expected, exception.Message, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}