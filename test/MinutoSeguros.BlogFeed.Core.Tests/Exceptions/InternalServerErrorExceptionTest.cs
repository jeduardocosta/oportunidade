using System;
using FluentAssertions;
using NUnit.Framework;
using MinutoSeguros.BlogFeed.Core.Exceptions;

namespace MinutoSeguros.BlogFeed.Core.Tests.Exceptions
{
    [TestFixture]
    public class InternalServerErrorExceptionTest
    {
        [Test]
        public void Should_ReturnAnExactErrorMessage_InInternalServerErrorException()
        {
            const string expected = "Internal server error.";

            Action action = () => { throw new InternalServerErrorException(); };

            action
                .ShouldThrow<InternalServerErrorException>()
                .WithMessage(expected);
        }
    }
}