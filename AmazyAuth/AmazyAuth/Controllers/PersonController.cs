using AmazyAuth.Context;
using AmazyAuth.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web;

namespace AmazyAuth.Controllers
{
    public class PersonController : Controller
    {
        //private readonly ILogger<PersonController> _logger;

        //public PersonController(ILogger<PersonController> logger)
        //{
        //    _logger = logger;
        //}

        private List<Person> people = new List<Person>
        {
            new Person {Login="admin@gmail.com", Password="12345" },
            new Person { Login="qwerty@gmail.com", Password="55555" }
        };

        //[HttpGet(Name = "GetPerson")]
        //public Person Get()
        //{
        //    var person = new Person { Login = "Eugen", Password = "1221" };

        //    using (ApplicationContext context = new ApplicationContext())
        //    {
        //        context.Persons.Add(person);
        //        context.SaveChanges();
        //    }

        //    return person;
        //}

        [HttpPost("/token")]
        public IActionResult Token(string login, string password)
        {
            var identity = GetIdentity(login, password);

            if (identity == null)
                return BadRequest(new { errorText = "Invalid username or password" });

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var canEncodedJwt = jwtSecurityTokenHandler.CanWriteToken;
            if (canEncodedJwt)
            {
                var encodedJwt = jwtSecurityTokenHandler.WriteToken(jwt);
                var response = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };

                return Json(response);
            }

            return null;
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            Person person = people.FirstOrDefault(n => n.Login == login && n.Password == password);
            if(person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }
            return null;
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