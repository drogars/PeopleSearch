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
using PeopleSearch.Infrastructure.Services;

namespace PeopleSearch.Server.Server
{
    public class PersonCommandsApiController : ApiController
    {
        private readonly PeopleContext _peopleContext;
        private readonly IPersonCommandsService _personCommandsService;

        public PersonCommandsApiController(PeopleContext peopleContext, IPersonCommandsService personCommandsService)
        {
            _peopleContext = peopleContext;
            _personCommandsService = personCommandsService;
        }

        [HttpGet]
        public HttpResponseMessage SeedPeople()
        {
            var picture = GetByteArrayForImage(@"~/App_Data/images/DarthVader.jpg");
            _personCommandsService.SavePerson("Darth", "Vader", new DateTime(1937, 5, 4), new List<string> {"Sith", "Luke Skywalker", "Force"}, picture, "421 S Vader Way", "Suite 600", "Coruscant", "UT", "90000");
            
            picture = GetByteArrayForImage(@"~/App_Data/images/HanSolo.jpg");
            _personCommandsService.SavePerson("Han", "Solo", new DateTime(1947, 5, 4), new List<string> { "Nerf herding", "Princess Leia", "Kessel Spiece Run" }, picture, "12 Parsec Ave", null, "Milennium Falcon", "UT", "12001");
            
            picture = GetByteArrayForImage(@"~/App_Data/images/LukeSkywalker.jpg");
            _personCommandsService.SavePerson("Luke", "Skywalker", new DateTime(1959, 5, 4), new List<string> { "Force", "Princess Leia", "Power converters" }, picture, "Moisture Farm", null, "Mos Eisley", "UT", "12345");
            
            picture = GetByteArrayForImage(@"~/App_Data/images/PrincessLeia.jpg");
            _personCommandsService.SavePerson("Princess", "Leia", new DateTime(1959, 5, 4), new List<string> { "The rebellion", "Restoring the imperial senate", "Alderaan" }, picture, "1 Palace Way", "The Palace", "Capital City", "UT", "43125");
            
            _peopleContext.SaveChanges();

            var result = new SeedPeopleResult {NumberOfPeopleCreated = 4};

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
        public int NumberOfPeopleCreated { get; set; }
    }
}
