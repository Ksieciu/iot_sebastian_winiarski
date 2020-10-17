using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1_winiarski.Rest.Context;
using Lab1_winiarski.Rest.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1_winiarski.Rest.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PeopleController : Controller
    {

        private readonly AzureDbContext context;

        public PeopleController(AzureDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        [Route("")]
        public List<Person> Get()
        {
            return context.People.ToList();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Person person)
        {

            if (ModelState.IsValid)
            {
                context.People.Add(person);
                context.SaveChanges();
                return Ok(person);
            }
            return BadRequest("Person creation failed");
        }


        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromBody] Person person)
        {
            if (ModelState.IsValid)
            {
                context.People.Update(person);
                context.SaveChanges();
                return Ok(person);
            }
            return BadRequest("Update failed");
        }


        [HttpGet]
        [Route("{id:int}")]
        public Person Details([FromRoute] int id)
        {
            return context.People.FirstOrDefault(x => x.PersonId == id);  
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id, Person person)
        {
            int p = context.People.Count(x => x.PersonId == id);

            if (p > 0)
            {
                context.People.Remove(person);
                context.SaveChanges();
                return Ok("Object deleted");
            }
            return BadRequest("No objects deleted");
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
