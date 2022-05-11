using _03_Demenagements.Datas;
using _03_Demenagements.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _03_Demenagements.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PersonsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("/person")]
        public IActionResult AddPerson(Person newPerson)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            context.Persons.Add(newPerson);

            if (context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "La personne a été ajoutée en base de données !",
                    Person = newPerson
                });
            }

            return BadRequest(new
            {
                Message = "Ajout impossible de la personne en base de données !"
            });
        }

        [HttpGet("/persons")]
        public IActionResult GetAllPersons()
        {
            var persons = context.Persons.Include(x => x.Properties).ToList();

            if (persons.Count > 0)
            {
                return Ok(new
                {
                    Message = "Voici la liste des personnes :",
                    Persons = persons
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a pas de personne en base de données !"
            });
        }

        [HttpGet("/person/{id}")]
        public IActionResult GetPersonById(int id)
        {
            var person = context.Persons.Include(x => x.Properties).SingleOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a personne avec cet Id dans la base de données !"
                });
            }

            return Ok(new
            {
                Message = "Voici la personne demandée :",
                Person = person
            });
        }

        [HttpPatch("/person/{id}")]
        public IActionResult EditPersonById(int id, Person newValues)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var found = context.Persons.FirstOrDefault(x => x.Id == id);
            
            if (found != null)
            {
                found.FirstName = newValues.FirstName;
                found.LastName = newValues.LastName;
                found.Email = newValues.Email;
                found.PhoneNumber = newValues.PhoneNumber;

                if (context.SaveChanges() > 0)
                {
                    return Ok(new
                    {
                        Message = "La personne a été éditée avec succès !",
                        Person = found
                    });

                }

                return BadRequest(new
                {
                    Message = "Edition impossible de la personne dans la base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a personne en base de données avec cet Id !"
            });

        }

        [HttpDelete("/person/{id}")]
        public IActionResult DeletePersonById(int id)
        {
            var found = context.Persons.FirstOrDefault(x => x.Id == id);

            if (found != null)
            {
                context.Persons.Remove(found);

                if (context.SaveChanges() > 0)
                {
                    return Ok(new
                    {
                        Message = "La personne a été supprimée avec succès !"
                    });

                }

                return BadRequest(new
                {
                    Message = "Suppression impossible de la personne dans la base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a personne en base de données avec cet Id !"
            });
        }
    }
}
