using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PeopleSearch.Domain.Entities;
using PeopleSearch.Domain.Services;
using PeopleSearch.Infrastructure.Dto;
using PeopleSearch.Server.Server;

namespace PeopleSearch.UnitTests.Controllers
{
    [TestFixture]
    public class PersonQueriesApiControllerTests
    {
        [Test]
        public void SearchPeople_ShouldReturnBadRequest_WhenSearchCriteriaAreInvalid()
        {
            // Arrange
            var personRepository = new Mock<IPersonRepository>();
            var personQueriesApiController = new PersonQueriesApiController(personRepository.Object);
            personQueriesApiController.Request =  new HttpRequestMessage();
            personQueriesApiController.Request.SetConfiguration(new HttpConfiguration());

            // Act
            var response = personQueriesApiController.SearchPeople("");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void SearchPeople_ShouldReturnInternalServerError_WhenUnableToRetrievePeople()
        {
            // Arrange
            var personRepository = new Mock<IPersonRepository>();
            personRepository.Setup(p => p.Search(It.IsAny<string>())).Throws<Exception>();
            var personQueriesApiController = new PersonQueriesApiController(personRepository.Object);
            personQueriesApiController.Request = new HttpRequestMessage();
            personQueriesApiController.Request.SetConfiguration(new HttpConfiguration());

            // Act
            var response = personQueriesApiController.SearchPeople("Darth Vader");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Test]
        public void SearchPeople_ShouldCallPersonRepistorySearchWithSearchCriteria_WhenGivenValidSearchCriteria()
        {
            // Arrange
            var searchCriteria = "Darth Vader";
            var personRepository = new Mock<IPersonRepository>();
            personRepository
                .Setup(r => r.Search(It.Is((string criteria) => criteria == searchCriteria)))
                .Returns(new List<Person> {Person.CreatePerson("Darth", "Vader", new DateTime(1976, 5, 4), null, null, null, null, null, null, null)});
            var personQueriesApiController = new PersonQueriesApiController(personRepository.Object);
            personQueriesApiController.Request = new HttpRequestMessage();
            personQueriesApiController.Request.SetConfiguration(new HttpConfiguration());

            // Act
            var response = personQueriesApiController.SearchPeople(searchCriteria);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            personRepository.Verify(r => r.Search(It.Is((string criteria) => criteria == searchCriteria)), Times.Exactly(1));
        }

        [Test]
        public void SearchPeopleSlow_ShouldReturnBadRequest_WhenSearchCriteriaAreInvalid()
        {
            // Arrange
            var personRepository = new Mock<IPersonRepository>();
            var personQueriesApiController = new PersonQueriesApiController(personRepository.Object);
            personQueriesApiController.Request = new HttpRequestMessage();
            personQueriesApiController.Request.SetConfiguration(new HttpConfiguration());

            // Act
            var response = personQueriesApiController.SearchPeopleSlow("");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public void SearchPeopleSlow_ShouldReturnInternalServerError_WhenUnableToRetrievePeople()
        {
            // Arrange
            var personRepository = new Mock<IPersonRepository>();
            personRepository.Setup(p => p.Search(It.IsAny<string>())).Throws<Exception>();
            var personQueriesApiController = new PersonQueriesApiController(personRepository.Object);
            personQueriesApiController.Request = new HttpRequestMessage();
            personQueriesApiController.Request.SetConfiguration(new HttpConfiguration());

            // Act
            var response = personQueriesApiController.SearchPeopleSlow("Darth Vader");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Test]
        public void SearchPeopleSlow_ShouldCallPersonRepistorySearchWithSearchCriteria_WhenGivenValidSearchCriteria()
        {
            // Arrange
            var searchCriteria = "Darth Vader";
            var personRepository = new Mock<IPersonRepository>();
            personRepository
                .Setup(r => r.Search(It.Is((string criteria) => criteria == searchCriteria)))
                .Returns(new List<Person> { Person.CreatePerson("Darth", "Vader", new DateTime(1976, 5, 4), null, null, null, null, null, null, null) });
            var personQueriesApiController = new PersonQueriesApiController(personRepository.Object);
            personQueriesApiController.Request = new HttpRequestMessage();
            personQueriesApiController.Request.SetConfiguration(new HttpConfiguration());

            // Act
            var response = personQueriesApiController.SearchPeopleSlow(searchCriteria);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            personRepository.Verify(r => r.Search(It.Is((string criteria) => criteria == searchCriteria)), Times.Exactly(1));
        }
    }
}
