using Autofac;
using MinutoSeguros.BlogFeed.Core.Helpers;
using MinutoSeguros.BlogFeed.Core.Parsers;
using MinutoSeguros.BlogFeed.Core.Readers;
using MinutoSeguros.BlogFeed.Log.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinutoSeguros.BlogFeed.Core.DI
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BlogFeedReader>().As<IBlogFeedReader>();
            builder.RegisterType<BlogFeedContentParser>().As<IBlogFeedContentParser>();
            builder.RegisterType<HtmlHelper>().As<IHtmlHelper>();
            builder.RegisterType<UrlHelper>().As<IUrlHelper>();

            builder.RegisterModule(new LogModule());
        }
    }
}
