using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.Domain.Services
{
    public interface IInterestRepository
    {
        Interest Get(int id);

        Interest GetByValue(string interest);

        IEnumerable<Interest> GetByValues(IEnumerable<string> interests);

        Interest Add(Interest entity);

        void Delete(Interest entity);

        void Save();
    }
}
