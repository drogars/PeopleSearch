using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PeopleSearch.Domain.Entities;
using PeopleSearch.Domain.Services;
using PeopleSearch.Infrastructure.Dto;

namespace PeopleSearch.Server.Server
{
    public class PersonQueriesApiController : ApiController
    {
        private readonly IPersonRepository _personRepository;

        public PersonQueriesApiController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public HttpResponseMessage SearchPeople(string searchCriteria)
        {
            if (string.IsNullOrEmpty(searchCriteria))
            {
                var message = "Please provide search criteria";
                return Request.CreateResponse(HttpStatusCode.BadRequest, message);
            }

            List<PersonDto> results;
            try
            {
                results = _personRepository
                    .Search(searchCriteria)
                    .Select(p => new PersonDto(p))
                    .ToList();
            }
            catch (Exception ex)
            {
                var message = string.Format("There was a problem performing the search: {0}", ex.ToString());
                Debug.Write(message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        [HttpGet]
        public HttpResponseMessage SearchPeopleSlow(string searchCriteria)
        {
            // Simulate a slow server response
            System.Threading.Thread.Sleep(6000);

            var response = SearchPeople(searchCriteria);

            return response;
        }
    }
    
    public class QueryErrorResult
    {
        public string Message { get; set; }
    }
}
