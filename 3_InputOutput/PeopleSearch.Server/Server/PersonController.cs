using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PeopleSearch.Domain;
using PeopleSearch.Domain.Entities;
using PeopleSearch.Infrastructure;

namespace PeopleSearch.Server.Server
{
    public class PersonController : ApiController
    {
        public HttpResponseMessage GetPerson(int personId)
        {
            Person person = Person.CreatePerson("Darth", "Vader", new DateTime(2015, 5, 4));

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

        [HttpGet]
        public HttpResponseMessage SeedPeople()
        {
            var peopleContext = new PeopleContext();

            var person = Person.CreatePerson("Darth", "Vader", DateTime.Today);

            peopleContext.People.Add(person);
            peopleContext.SaveChanges();

            var result = new SeedPeopleResult();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }

    public class SeedPeopleResult
    {

    }

    public class QueryErrorResult
    {
        public string Message { get; set; }
    }
}
