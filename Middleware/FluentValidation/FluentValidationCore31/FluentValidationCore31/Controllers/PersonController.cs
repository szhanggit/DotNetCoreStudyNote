using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationCore31.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            PersonModel person = new PersonModel() { FirstName = "Dave", LastName = "Davis" };
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody]PersonModel person)
        {
            return Ok($"{person.FirstName} {person.LastName}");
        }
    }
}