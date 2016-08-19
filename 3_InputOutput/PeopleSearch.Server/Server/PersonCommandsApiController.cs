using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using PeopleSearch.Domain.Entities;
using PeopleSearch.Infrastructure;

namespace PeopleSearch.Server.Server
{
    public class PersonCommandsApiController : ApiController
    {
        private readonly PeopleContext _peopleContext;

        public PersonCommandsApiController(PeopleContext peopleContext)
        {
            _peopleContext = peopleContext;
        }

        [HttpGet]
        public HttpResponseMessage SeedPeople()
        {
            var picture = GetByteArrayForImage(@"~/App_Data/images/DarthVader.jpg");
            var person = Person.CreatePerson("Darth", "Vader", new DateTime(1937, 5, 4), new List<string> { "Sith", "Luke Skywalker", "Force" }, picture, null, null, null, null, null);
            _peopleContext.People.Add(person);

            picture = GetByteArrayForImage(@"~/App_Data/images/HanSolo.jpg");
            person = Person.CreatePerson("Han", "Solo", new DateTime(1947, 5, 4), new List<string> { "Nerf herding", "Princess Leia", "Kessel Spiece Run" }, picture, null, null, null, null, null);
            _peopleContext.People.Add(person);

            picture = GetByteArrayForImage(@"~/App_Data/images/LukeSkywalker.jpg");
            person = Person.CreatePerson("Luke", "Skywalker", new DateTime(1959, 5, 4), new List<string> { "Force", "Princess Leia", "Power converters" }, picture, null, null, null, null, null);
            _peopleContext.People.Add(person);

            picture = GetByteArrayForImage(@"~/App_Data/images/PrincessLeia.jpg");
            person = Person.CreatePerson("Princess", "Leia", new DateTime(1959, 5, 4), new List<string> { "The rebellion", "Restoring the imperial senate", "Alderaan" }, picture, null, null, null, null, null);
            _peopleContext.People.Add(person);

            _peopleContext.SaveChanges();

            var result = new SeedPeopleResult();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        private static byte[] GetByteArrayForImage(string imagePath)
        {
            var path = HostingEnvironment.MapPath(imagePath);
            if (string.IsNullOrEmpty(path))
            {
                return new byte[0];
            }

            var picture = Image.FromFile(path);
            using (var memoryStream = new MemoryStream())
            {
                picture.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }
    }

    public class SeedPeopleResult
    {

    }
}
