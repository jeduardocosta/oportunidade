using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinutoSeguros.BlogFeed.API.Controllers;
using MinutoSeguros.BlogFeed.API.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.API.Tests.DI
{
    [TestClass]
    public class DIContainerTest
    {
        [TestMethod]
        public void Should_ResolveDependencies_UsingDIContainerClass()
        {
            var controller = DIContainer.Resolve<PostsController>();

            Assert.IsNotNull(controller);
        }
    }
}