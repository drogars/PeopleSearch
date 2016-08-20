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
using PeopleSearch.Infrastructure.Services;

namespace PeopleSearch.UnitTests.Services
{
    [TestFixture]
    public class PersonCommandsServiceTests
    {
        [Test]
        public void SavePerson_ShouldGracefullyHandleANullInterestList()
        {
            // Arrange 
            var mockPersonRepository = new Mock<IPersonRepository>();
            var mockInterestRepository = new Mock<IInterestRepository>();
            var personCommandsService = new PersonCommandsService(mockPersonRepository.Object, mockInterestRepository.Object);

            // Act // Assert
            Assert.DoesNotThrow(() => personCommandsService.SavePerson("Dark", "Vader", new DateTime(1977, 5, 4), null, null, null, null, null, null, null));
        }

        [Test]
        public void SavePerson_ShouldEnsureDuplicateInterestsAreNotSaved_WhenANewPersonIsSaved()
        {
            // Arrange
            var mockInterestRepository = new MockInterestRepository();
            mockInterestRepository.Add(Interest.Create("C#"));
            mockInterestRepository.Add(Interest.Create("Linq"));
            //mockInterestRepository.Add(Interest.Create("C#"));
            var mockPersonRepository = new Mock<IPersonRepository>();
            var personCommandsService = new PersonCommandsService(mockPersonRepository.Object, mockInterestRepository);
            var interests = new List<string> {"C#", "The Sith", "The Dark Side of the Force"};
            var numOfInterestsBeforeSave = mockInterestRepository.InterestList.Count;

            // Act
            personCommandsService.SavePerson("Darth", "Vader", new DateTime(1977, 5, 4), interests, new byte[0], null, null, null, null, null);

            // Assert
            mockInterestRepository.InterestList.Count.Should().Be(numOfInterestsBeforeSave + 2);
        }

        [Test]
        public void SavePerson_ShouldCallRepostoryAddAndSave_WhenGivenAValidOfPersonInformation()
        {
            // Arrange
            var interests = new List<string> { "C#", "The Sith", "The Dark Side of the Force" };
            var mockInterestRepository = new Mock<IInterestRepository>();
            mockInterestRepository
                .Setup(r => r.Add(It.IsAny<Interest>()))
                .Verifiable();
            mockInterestRepository
                .Setup(r => r.Add(It.IsAny<Interest>()))
                .Verifiable();
            mockInterestRepository
                .Setup(r => r.Add(It.IsAny<Interest>()))
                .Verifiable();

            var mockPersonRepository = new Mock<IPersonRepository>();
            mockPersonRepository
                .Setup(r => r.Add(It.IsAny<Person>())).Verifiable();
            mockPersonRepository
                .Setup(r => r.Save()).Verifiable();

            var personCommandsService = new PersonCommandsService(mockPersonRepository.Object, mockInterestRepository.Object);

            var firstName = "Darth";
            var lastName = "Vader";
            var dateOfBirth = new DateTime(2015, 5, 4);

            // Act
            var person = personCommandsService.SavePerson(firstName, lastName, dateOfBirth, interests, null, null, null, null, null, null);

            // Assert
            mockInterestRepository.VerifyAll();
            mockPersonRepository.VerifyAll();
            person.Should().NotBeNull();
            person.FirstName.Should().Be(firstName);
            person.LastName.Should().Be(lastName);
            person.DateOfBirth.Should().Be(dateOfBirth);
            person.Interests.Count.Should().Be(interests.Count);
        }
    }

    public class MockInterestRepository : IInterestRepository
    {
        public MockInterestRepository()
        {
            InterestList = new List<Interest>();
        }

        public List<Interest> InterestList { get; }

        public Interest Get(int id)
        {
            return InterestList.SingleOrDefault(i => i.InterestId == id);
        }

        public Interest GetByValue(string interest)
        {
            return InterestList.SingleOrDefault(i => i.Value == interest);
        }

        public IEnumerable<Interest> GetByValues(IEnumerable<string> interests)
        {
            return InterestList.Where(i => interests.Contains(i.Value)).ToList();
        }

        public Interest Add(Interest entity)
        {
            InterestList.Add(entity);

            return entity;
        }

        public void Delete(Interest entity)
        {
            InterestList.Remove(entity);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
