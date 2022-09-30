using AmazyAuth.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AmazyAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPerson")]
        public Person Get()
        {
            var person = new Person { Login = "Eugen", Password = "1221" };

            using (ApplicationContext context = new ApplicationContext())
            {
                context.Persons.Add(person);
                context.SaveChanges();
            }

            return person;
        }

        [HttpPost(Name = "PostPerson")]
        public Person Post([FromBody] Person person)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Persons.Add(person);
                context.SaveChanges();
            }

            return person;
        }

        [HttpPut(Name = "PutPerson")]
        public bool Put([FromBody] Person person)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var candidate = context.Persons.FirstOrDefault(n => n.Login == person.Login && n.Password == person.Password);
                return candidate != null;
            }
        }
    }
}