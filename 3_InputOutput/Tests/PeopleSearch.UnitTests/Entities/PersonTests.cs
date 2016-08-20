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
            Assert.Throws<ArgumentException>(() => Person.CreatePerson(firstName, lastName, dateOfBirth, null, null, null, null, null, null, null));


            // Arrange
            firstName = "Darth";
            lastName = "";
            dateOfBirth = new DateTime(2010, 5, 4);

            // Act // Assert
            Assert.Throws<ArgumentException>(() => Person.CreatePerson(firstName, lastName, dateOfBirth, null, null, null, null, null, null, null));


            // Arrange
            firstName = "Darth";
            lastName = "Vader";
            dateOfBirth = new DateTime(2010, 5, 4, 4, 4, 4);

            // Act // Assert
            Assert.Throws<ArgumentException>(() => Person.CreatePerson(firstName, lastName, dateOfBirth, null, null, null, null, null, null, null));
        }

        [Test]
        public void Create_ShouldSetInterestsToEmptyList_WhenPassedANullObject()
        {
            // Arrange
            var firstName = "Darth";
            var lastName = "Vader";
            var dateOfBirth = new DateTime(2010, 5, 4);
            List<Interest> interests = null;

            // Act
            var person = Person.CreatePerson(firstName, lastName, dateOfBirth, interests, null, null, null, null, null, null);

            // Assert
            person.Interests.Should().NotBeNull();
            person.Interests.Count.Should().Be(0);
        }

        [Test]
        public void Create_ShouldSetPictureToEmptyArray_WhenPassedANullObject()
        {
            // Arrange
            var firstName = "Darth";
            var lastName = "Vader";
            var dateOfBirth = new DateTime(2010, 5, 4);
            var interests = new List<Interest>();
            byte[] picture = null;

            // Act
            var person = Person.CreatePerson(firstName, lastName, dateOfBirth, interests, picture, null, null, null, null, null);

            // Assert
            person.Picture.Should().NotBeNull();
            person.Picture.Length.Should().Be(0);
        }

        [Test]
        public void Create_ShouldSetProperties_WhenPassedValidValues()
        {
            // Arrange
            var firstName = "Darth";
            var lastName = "Vader";
            var dateOfBirth = new DateTime(2010, 5, 4);
            var addr1 = "400 E 200 S";
            var addr2 = "";
            var city = "Salt Lake City";
            var state = "UT";
            var postalCode = "84101";
            var interestValues = new List<string> {"c#", "TypeScript", "npm", "cycling"};
            var interests = interestValues.Select(i => Interest.Create(i)).ToList();
            var picture = new byte[] {31, 2, 7};

            // Act
            var person = Person.CreatePerson(firstName, lastName, dateOfBirth, interests, picture, addr1, addr2, city, state, postalCode);

            // Assert
            person.FirstName.Should().Be(firstName);
            person.LastName.Should().Be(lastName);
            person.DateOfBirth.Should().Be(dateOfBirth);
            person.Address1.Should().Be(addr1);
            person.Address2.Should().Be(addr2);
            person.City.Should().Be(city);
            person.State.Should().Be(state);
            person.PostalCode.Should().Be(postalCode);
            foreach (var interest in person.Interests)
            {
                interests.Should().Contain(i => i.Value == interest.Value);
            }
            person.Picture.Should().BeSameAs(picture);
        }

        
    }
}
