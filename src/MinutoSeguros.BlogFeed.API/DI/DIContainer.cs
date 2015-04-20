using Autofac;
using Autofac.Integration.WebApi;
using MinutoSeguros.BlogFeed.Core.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MinutoSeguros.BlogFeed.API.DI
{
    public class DIContainer
    {
        private static IContainer _container;

        public static IContainer Container
        {
            get
            {
                if (_container == null) 
                    Setup();

                return _container;
            }
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        private static void Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ApiModule());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            _container = builder.Build();
        }
    }
}