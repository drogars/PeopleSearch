using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.UnitTests.Entities
{
    [TestFixture]
    public class InterestTests
    {
        [Test]
        public void Create_ShouldValidateParameters()
        {
            // Arrange // Act // Assert
            Assert.Throws<ArgumentException>(() => Interest.Create(""));

            // Arrange // Act // Assert
            Assert.Throws<ArgumentException>(() => Interest.Create(null));
        }

        [Test]
        public void Create_ShouldSetProperties_WhenPassedValidValues()
        {
            // Arrange
            var interestText = "c#";

            // Act
            var interest = Interest.Create(interestText);

            // Assert
            interest.Value.Should().Be(interestText);
        }
    }
}
