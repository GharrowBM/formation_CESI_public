using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _03_Repertoire.Datas;
using _03_Repertoire.Models;
using Microsoft.AspNetCore.Authorization;

namespace TP04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ContactsController : ControllerBase
    {
        private ApplicationDbContext context;

        public ContactsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("/contacts")]
        [Authorize(Roles = "User")]
        public IActionResult GetAll()
        {
            var contacts = context.Contacts.ToList();

            if (contacts.Count > 0)
            {
                return Ok(new { Contacts = contacts });
            }
            else
            {
                return NotFound(new { Message = "Le répertoire est vide !" });
            }
        }

        [HttpGet("/contact/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult GetOne(int id)
        {
            var contact = context.Contacts.FirstOrDefault(x => x.Id == id);

            if (contact == null) return NotFound(new { Message = "Aucun contact ne possède cet ID !" });
            else
            {
                return Ok(new { Contact = contact });
            }
        }

        [HttpPost("/contact")]
        public IActionResult Create(Contact newContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Contacts.Add(newContact);

            if (context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "Le contact a été ajouté en base de données avec succès !",
                    Contact = newContact
                });
            }

            return BadRequest(new
            {
                Message = "Une erreur s'est produite lors de l'ajout en base de donnée !"
            });
        }

        [HttpPut("/contact/{id}")]
        public IActionResult Edit(int id, Contact newValues)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = context.Contacts.FirstOrDefault(x => x.Id == id);

            if (contact == null) 
                return NotFound(new { Message = "Aucun contact ne possède cet ID !" });

            contact.Firstname = newValues.Firstname;
            contact.Lastname = newValues.Lastname;
            contact.Email = newValues.Email;
            contact.Gender = newValues.Gender;
            contact.Phone = newValues.Phone;

            if (context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "LE contact a été modifié avec succès !",
                    Contact = contact
                });
            }

            return BadRequest(new
            {
                Message = "Une erreur s'est produite lors de l'édition du contact dans la base de données !"
            });
        }

        [HttpDelete("/contact/{id}")]
        public IActionResult Delete(int id)
        {
            var contact = context.Contacts.FirstOrDefault(x => x.Id == id);

            if (contact == null)
                return NotFound(new { Message = "Aucun contact ne possède cet ID !" });

            context.Contacts.Remove(contact);

            if (context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "LE contact a été supprimé avec succès !",
                    Contact = contact
                });
            }

            return BadRequest(new
            {
                Message = "Une erreur s'est produite lors de la suppression du contact dans la base de données !"
            });
        }
    }
}
