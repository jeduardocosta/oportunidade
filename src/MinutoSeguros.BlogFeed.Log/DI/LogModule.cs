using Autofac;

namespace MinutoSeguros.BlogFeed.Log.DI
{
    public class LogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoggerWrapper>().As<ILogger>();
        }
    }
}