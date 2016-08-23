using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.Infrastructure.Dto
{
    public class PersonDto
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public ICollection<string> Interests { get; set; }
        public byte[] Picture { get; set; }

        public PersonDto(Person person)
        {
            if (person == null) throw new ArgumentException("person can't be null", nameof(person));

            PersonId = person.PersonId;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Age = CalculateAge(person.DateOfBirth);
            Address1 = person.Address1;
            Address2 = person.Address2;
            City = person.City;
            State = person.State;
            PostalCode = person.PostalCode;
            Interests = person.Interests.Select(i => i.Value).ToList();
            Picture = person.Picture;
        }

        public static string CalculateAge(DateTime dateOfBirth)
        {
            var now = SystemClock.Instance.Now;
            var tz = DateTimeZoneProviders.Tzdb.GetSystemDefault();

            var today = now.InZone(tz).Date;
            var nodaDateOfBirth = new LocalDate(dateOfBirth.Year, dateOfBirth.Month, dateOfBirth.Day);

            var period = Period.Between(nodaDateOfBirth, today, PeriodUnits.Years | PeriodUnits.Days);

            return string.Format("{0} years, {1} days", period.Years, period.Days);
        }
    }
}
