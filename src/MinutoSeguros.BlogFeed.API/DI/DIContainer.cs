using System;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;

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
                { 
                    Setup();
                }

                return _container;
            }
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static object Resolve(Type type)
        {
            return Container.Resolve(type);
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