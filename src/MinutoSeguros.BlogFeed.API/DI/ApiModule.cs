using Autofac;
using MinutoSeguros.BlogFeed.API.Parsers;
using MinutoSeguros.BlogFeed.Core.DI;

namespace MinutoSeguros.BlogFeed.API.DI
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestParametersParser>().As<IRequestParametersParser>();

            builder.RegisterModule(new CoreModule());
        }
    }
}