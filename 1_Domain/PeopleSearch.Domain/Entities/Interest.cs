using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleSearch.Domain.Entities
{
    public class Interest
    {
        public int InterestId { get; protected set; }
        public string Value { get; protected set; }

        protected Interest()
        {
            
        }

        public static Interest Create(string interest)
        {
            if (string.IsNullOrEmpty(interest)) throw new ArgumentException("interest text is required", nameof(interest));

            return new Interest {Value = interest};
        }
    }
}
