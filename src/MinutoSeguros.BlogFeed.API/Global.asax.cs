using Autofac;
using Autofac.Integration.WebApi;
using MinutoSeguros.BlogFeed.API.DI;
using MinutoSeguros.BlogFeed.Log;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace MinutoSeguros.BlogFeed.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            ILogger logger = null;

            try
            {
                logger = new LoggerWrapper();

                var config = GlobalConfiguration.Configuration;
                config.DependencyResolver = new AutofacWebApiDependencyResolver(DIContainer.Container);
            }
            catch (Exception exception)
            {
                const string errorMessage = "Failed to load base classes.";

                if (logger != null)
                    logger.Error(errorMessage, exception);
                
                throw new Exception(errorMessage);
            }
        }
    }
}