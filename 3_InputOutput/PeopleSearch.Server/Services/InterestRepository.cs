using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PeopleSearch.Domain.Entities;
using PeopleSearch.Domain.Services;
using PeopleSearch.Infrastructure;

namespace PeopleSearch.Server.Services
{
    public class InterestRepository : IInterestRepository, IDisposable
    {
        private bool _disposed = false;

        private readonly PeopleContext _peopleContext;

        public InterestRepository(PeopleContext dbContext)
        {
            _peopleContext = dbContext;
        }

        public Interest Get(int id)
        {
            return _peopleContext.Interests.Find(id);
        }

        public Interest GetByValue(string interest)
        {
            return _peopleContext.Interests.SingleOrDefault(i => i.Value == interest);
        }

        public IEnumerable<Interest> GetByValues(IEnumerable<string> interests)
        {
            return _peopleContext.Interests.Where(i => interests.Contains(i.Value)).ToList();
        }

        public Interest Add(Interest entity)
        {
            return _peopleContext.Interests.Add(entity);
        }

        public void Delete(Interest entity)
        {
            _peopleContext.Interests.Remove(entity);
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