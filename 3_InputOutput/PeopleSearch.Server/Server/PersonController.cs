using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PeopleSearch.Domain;

namespace PeopleSearch.Server.Server
{
    public class PersonController : ApiController
    {
        public HttpResponseMessage GetPerson(int personId)
        {
            Person person = Person.CreatePerson("Darth", "Vader");

            try
            {

            }
            catch (Exception ex)
            {
                var errorMessage = string.Format("An error occurred while trying to get the person: {0}.", personId);

                // TODO: log error message and exception

                return Request.CreateResponse(HttpStatusCode.InternalServerError, new QueryErrorResult {Message = errorMessage});
            }

            return Request.CreateResponse(HttpStatusCode.OK, person);
        }
    }

    public class QueryErrorResult
    {
        public string Message { get; set; }
    }
}
