using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PeopleSearch.Domain.Entities;
using PeopleSearch.Domain.Services;
using PeopleSearch.Infrastructure.Dto;
using PeopleSearch.Infrastructure.Services;

namespace PeopleSearch.UnitTests.Dto
{
    [TestFixture]
    public class PersonDtoTests
    {
        [Test]
        public void Constructor_ShouldValidateParameters()
        {
            // Arrange

            // Act // Assert
            Assert.Throws<ArgumentException>(() => new PersonDto(null));
        }

        [Test]
        public void Constructor_ShouldInitializeProperties_WhenPassedAValidPersonEntity()
        {
            // Arrange
            var firstName = "Darth";
            var lastName = "Vader";
            var dateOfBirth = new DateTime(1976, 5, 4);
            var interestValues = new List<string> {"Luke Skywalker", "The Force", "The Sith"};
            var interests = interestValues.Select(iv => Interest.Create(iv)).ToList();
            var address1 = "TIE Figher";
            var address2 = "Death Star";
            var city = "The Death Star";
            var state = "The Galactic Empire";
            var postalCode = "Everywhere";
            var picture = new byte[] { 23, 53, 12};

            var mockPersonRepository = new Mock<IPersonRepository>();
            var mockInterestRepository = new Mock<IInterestRepository>();
            mockInterestRepository.Setup(r => r.GetByValues(It.IsAny<List<string>>())).Returns(interests);
            var personCommandService = new PersonCommandsService(mockPersonRepository.Object, mockInterestRepository.Object);
            var person = personCommandService.SavePerson(firstName, lastName, dateOfBirth, interestValues, picture, address1,
                address2, city, state, postalCode);
            
            // Act
            var personDto = new PersonDto(person);

            // Assert
            personDto.PersonId.Should().Be(0);
            personDto.FirstName.Should().Be(firstName);
            personDto.LastName.Should().Be(lastName);
            personDto.Age.Should().Be(PersonDto.CalculateAge(dateOfBirth));
            personDto.Address1.Should().Be(address1);
            personDto.Address2.Should().Be(address2);
            personDto.City.Should().Be(city);
            personDto.State.Should().Be(state);
            personDto.PostalCode.Should().Be(postalCode);
            personDto.Interests.Should().BeEquivalentTo(interestValues);
            personDto.Picture.Should().BeEquivalentTo(picture);
        }
    }
}
