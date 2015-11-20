using System;
using FluentAssertions;
using NUnit.Framework;
using MinutoSeguros.BlogFeed.Core.Exceptions;

namespace MinutoSeguros.BlogFeed.Core.Tests.Exceptions
{
    [TestFixture]
    public class CustomErrorExceptionTest
    {
        [Test]
        public void Should_ReturnTheEntryMessage_InCustomErrorException()
        {
            const string expected = "sample error message";
            const string entry = "sample error message";

            Action action = () => { throw new CustomErrorException(entry); };

            action
                .ShouldThrow<CustomErrorException>()
                .WithMessage(expected);
        }
    }
}