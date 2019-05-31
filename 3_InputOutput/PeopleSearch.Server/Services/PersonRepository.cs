using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using PeopleSearch.Domain.Entities;
using PeopleSearch.Domain.Services;
using PeopleSearch.Infrastructure;

namespace PeopleSearch.Server.Services
{
    public class PersonRepository : IPersonRepository, IDisposable
    {
        private bool _disposed = false;

        private readonly PeopleContext _peopleContext;

        public PersonRepository(PeopleContext dbContext)
        {
            _peopleContext = dbContext;
        }
        
        public Person Get(int id)
        {
            return _peopleContext.People.Find(id);
        }

        public Person Add(Person entity)
        {
            return _peopleContext.People.Add(entity);
        }

        public List<Person> Search(string searchCriteria)
        {
            var searchTerms = searchCriteria.Split(' ');

            this._peopleContext.SaveChanges();

            var query = _peopleContext
                .People
                .Where(p => searchTerms.Any(s => p.FirstName.Contains(s)) || searchTerms.Any(s => p.LastName.Contains(s)))
                .Include(p => p.Interests);
            var people = query.ToList();

            return people;
        }

        public void Delete(Person entity)
        {
            _peopleContext.People.Remove(entity);
        }

        public void Save()
        {
            _peopleContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _peopleContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}