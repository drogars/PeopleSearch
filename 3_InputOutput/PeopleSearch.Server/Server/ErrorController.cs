using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Newtonsoft.Json;

namespace PeopleSearch.Server.Server
{
    public class ErrorController : ApiController
    {
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public HttpResponseMessage Handle404()
        {
            var requestedUrl = HttpContext.Current.Request.RawUrl;

            var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                ReasonPhrase = "The requested resource is not found"
            };
#if DEBUG
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(new { requestedUrl }));
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
#endif
            return responseMessage;
        }
    }

    /// <summary>
	/// By replacing the default controller selector with this implementation in <see cref="WebApiConfig"/>, we can route 
	/// requests for non-existent controllers to our custom 404 handler on the <see cref="ErrorController"/>.
	/// </summary>
    public class HttpNotFoundControllerSelector : DefaultHttpControllerSelector
    {
        public HttpNotFoundControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
        }
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            try
            {
                return base.SelectController(request);
            }
            catch (HttpResponseException ex)
            {
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound)
                    throw;
                var routeValues = request.GetRouteData().Values;
                routeValues["controller"] = "Error";
                routeValues["action"] = "Handle404";
                return base.SelectController(request);
            }
        }
    }

    /// <summary>
    /// By replacing the default action selector with this implementation in <see cref="WebApiConfig"/>, we can route 
    /// requests for non-existent actions to our custom 404 handler on the <see cref="ErrorController"/>.
    /// </summary>
    public class HttpNotFoundActionSelector : ApiControllerActionSelector
    {
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            try
            {
                return base.SelectAction(controllerContext);
            }
            catch (HttpResponseException ex)
            {
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound && code != HttpStatusCode.MethodNotAllowed)
                    throw;
                var routeData = controllerContext.RouteData;
                routeData.Values["action"] = "Handle404";
                IHttpController httpController = new ErrorController();
                controllerContext.Controller = httpController;
                controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "Error", httpController.GetType());
                return base.SelectAction(controllerContext);
            }
        }
    }
}