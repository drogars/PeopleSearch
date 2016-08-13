using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using PeopleSearch.Server.Server;

namespace PeopleSearch.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v10/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Remove application/xml so that it defaults to returning JSON data. 
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // Register custom WebApi exception handler
            config.Services.Replace(typeof(IExceptionHandler), new WebApiUnhandledExceptionHandler());

            // Register custom WebApi controller and action selectors
            config.Services.Replace(typeof(IHttpControllerSelector), new HttpNotFoundControllerSelector(config));
            config.Services.Replace(typeof(IHttpActionSelector), new HttpNotFoundActionSelector());

            config.Routes.MapHttpRoute(
                name: "Error404",
                routeTemplate: "api/{*url}",
                defaults: new { controller = "Error", action = "Handle404" }
            );
        }
    }

    public class WebApiUnhandledExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            // Causes exception to bubble up out of WebApi to ASP.NET. We need to catch it again in Global.asax Application_Error()
            context.Result = null;
        }
    }
}
