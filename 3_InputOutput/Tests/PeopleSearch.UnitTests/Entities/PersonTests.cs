using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PeopleSearch.Domain;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.UnitTests.Entities
{
    [TestFixture]
    public class PersonTests
    {
        [Test]
        public void Create_ShouldValidateParameters()
        {
            // Arrange
            string firstName = null;
            var lastName = "Vader";
            var dateOfBirth = new DateTime(2010, 5, 4);

            // Act // Assert
            Assert.Throws<ArgumentException>(() => Person.CreatePerson(firstName, lastName, dateOfBirth));


            // Arrange
            firstName = "Darth";
            lastName = "";
            dateOfBirth = new DateTime(2010, 5, 4);

            // Act // Assert
            Assert.Throws<ArgumentException>(() => Person.CreatePerson(firstName, lastName, dateOfBirth));


            // Arrange
            firstName = "Darth";
            lastName = "Vader";
            dateOfBirth = new DateTime(2010, 5, 4, 4, 4, 4);

            // Act // Assert
            Assert.Throws<ArgumentException>(() => Person.CreatePerson(firstName, lastName, dateOfBirth));
        }

        [Test]
        public void Create_ShouldSetProperties_WhenPassedValidValues()
        {
            // Arrange
            var firstName = "Darth";
            var lastName = "Vader";
            var dateOfBirth = new DateTime(2010, 5, 4);

            // Act
            var person = Person.CreatePerson(firstName, lastName, dateOfBirth);

            // Assert
            person.FirstName.Should().Be(firstName);
            person.LastName.Should().Be(lastName);
            person.DateOfBirth.Should().Be(dateOfBirth);
        }
    }
}
