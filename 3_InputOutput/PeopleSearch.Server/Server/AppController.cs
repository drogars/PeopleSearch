using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace PeopleSearch.Server.Server
{
    public class AppController : Controller
    {
        public ActionResult Index()
        {
            return File("/public/index.html", "text/html");
        }
    }
}
