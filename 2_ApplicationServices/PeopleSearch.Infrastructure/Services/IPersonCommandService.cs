using System;
using System.Collections.Generic;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.Infrastructure.Services
{
    public interface IPersonCommandsService
    {
        Person SavePerson(string firstName, string lastName, DateTime dateOfBirth, List<string> interestsValues, byte[] picture,
            string addr1, string addr2, string city, string state, string postalCode);
    }
}