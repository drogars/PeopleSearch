using System;

namespace PeopleSearch.Domain.Entities
{
    public class Person
    {
        public int PersonId { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }

        protected Person()
        {
            
        }

        public static Person CreatePerson(string firstName, string lastName, DateTime dateOfBirth)
        {
            //name, address, age, interests, and a picture
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException("First name is required.", nameof(firstName));
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentException("Last name is required.", nameof(lastName));
            if (dateOfBirth.TimeOfDay != TimeSpan.Zero) throw new ArgumentException("Date of birth should not contain time information", nameof(dateOfBirth));

            var person = new Person();

            person.FirstName = firstName;
            person.LastName = lastName;
            person.DateOfBirth = dateOfBirth;

            return person;
        }
    }
}
