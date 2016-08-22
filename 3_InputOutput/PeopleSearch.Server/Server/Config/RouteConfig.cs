using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PeopleSearch.Server.Server.Config
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                    name: "Default",
                    url: "{*segments}",
                    // Set the App controll as the default, which will return the index.html from the public directory provided by the WebUI project
                    defaults: new { controller = "App", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}