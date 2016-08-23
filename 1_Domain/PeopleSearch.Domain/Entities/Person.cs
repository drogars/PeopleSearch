using System;
using System.Collections.Generic;
using System.Linq;

namespace PeopleSearch.Domain.Entities
{
    public class Person
    {
        public int PersonId { get; protected set; }

        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        public DateTime DateOfBirth { get; protected set; }

        public string Address1 { get; protected set; }

        public string Address2 { get; protected set; }

        public string City { get; protected set; }

        public string State { get; protected set; }

        public string PostalCode { get; protected set; }

        public ICollection<Interest> Interests { get; protected set; }

        public byte[] Picture { get; protected set; }


        protected Person()
        {
            Interests = new List<Interest>();
        }

        public static Person CreatePerson(string firstName, string lastName, DateTime dateOfBirth, List<Interest> interests, byte[] picture,
            string addr1, string addr2, string city, string state, string postalCode)
        {
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException("First name is required.", nameof(firstName));
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentException("Last name is required.", nameof(lastName));
            if (dateOfBirth.TimeOfDay != TimeSpan.Zero) throw new ArgumentException("Date of birth should not contain time information", nameof(dateOfBirth));
            if (interests == null)
            {
                interests = new List<Interest>();
            }
            if (picture == null)
            {
                picture = new byte[0];
            }

            var person = new Person();

            person.FirstName = firstName;
            person.LastName = lastName;
            person.DateOfBirth = dateOfBirth;
            person.Address1 = addr1;
            person.Address2 = addr2;
            person.City = city;
            person.State = state;
            person.PostalCode = postalCode;
            person.Interests = interests;
            person.Picture = picture;

            return person;
        }
    }
}
