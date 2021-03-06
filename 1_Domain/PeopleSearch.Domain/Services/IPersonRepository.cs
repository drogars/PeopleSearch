﻿using System.Collections.Generic;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.Domain.Services
{
    public interface IPersonRepository
    {
        Person Get(int id);

        List<Person> Search(string searchCriteria);

        Person Add(Person entity);

        void Delete(Person entity);

        void Save();
    }
}