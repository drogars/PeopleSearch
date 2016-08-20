using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleSearch.Domain.Entities;
using PeopleSearch.Domain.Services;

namespace PeopleSearch.Infrastructure.Services
{
    public class PersonCommandsService : IPersonCommandsService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IInterestRepository _interestRepository;


        public PersonCommandsService(IPersonRepository personRepository, IInterestRepository interestRepository)
        {
            _personRepository = personRepository;
            _interestRepository = interestRepository;
        }

        public Person SavePerson(string firstName, string lastName, DateTime dateOfBirth, List<string> interestsValues, byte[] picture,
            string addr1, string addr2, string city, string state, string postalCode)
        {
            if (interestsValues == null)
            {
                interestsValues = new List<string>();
            }

            // get all the interests that already exist in the repository
            var interestsInRepo = _interestRepository.GetByValues(interestsValues).ToList();

            // get just the interst values
            var interestValuesInRepo = interestsInRepo.Select(i => i.Value).ToList();

            // get all the interests provided that are NOT already in the repository
            var interestValuesNotInRepo =
                interestsValues.Where(interestValue => interestValuesInRepo.All(interestValueInRepo => interestValueInRepo != interestValue));

            // construct the person's actual list of Interest objects
            var personsInterests = new List<Interest>();
            personsInterests.AddRange(interestsInRepo);
            foreach (var interestValue in interestValuesNotInRepo)
            {
                var interest = _interestRepository.Add(Interest.Create(interestValue));
                personsInterests.Add(interest);
            }

            var person = Person.CreatePerson(firstName, lastName, dateOfBirth, personsInterests, picture, addr1, addr2, city, state,
                postalCode);
            if (person != null)
            {
                _personRepository.Add(person);
                _personRepository.Save();
            }

            return person;
        }
    }
}
