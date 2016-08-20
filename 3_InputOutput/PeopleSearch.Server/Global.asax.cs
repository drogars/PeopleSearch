using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using PeopleSearch.Infrastructure;
using PeopleSearch.Infrastructure.Services;
using PeopleSearch.Server.Services;

namespace PeopleSearch.Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            builder.RegisterType<PeopleContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<PersonRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<InterestRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<PersonCommandsService>().AsImplementedInterfaces().InstancePerLifetimeScope();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            //builder.RegisterWebApiFilterProvider(config);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
