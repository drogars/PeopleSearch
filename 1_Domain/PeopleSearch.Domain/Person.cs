using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleSearch.Domain
{
    public class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public static Person CreatePerson(string firstName, string lastName)
        {
            var person = new Person();

            person.FirstName = firstName;
            person.LastName = lastName;

            return person;
        }
    }
}
