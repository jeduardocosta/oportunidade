using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.Core.Exceptions;

namespace MinutoSeguros.BlogFeed.Core.Tests.Exceptions
{
    [TestClass]
    public class CustomErrorExceptionTest
    {
        [TestMethod]
        public void Should_ReturnTheEntryMessage_InCustomErrorException()
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