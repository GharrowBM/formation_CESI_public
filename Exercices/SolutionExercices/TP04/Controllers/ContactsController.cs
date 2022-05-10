using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP04.Datas;
using TP04.Models;
using TP04.Services;

namespace TP04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IRepository<Contact> _contactsRepository;
        private UploadService _uploadService;

        public ContactsController(IRepository<Contact> contactsRepository, UploadService uploadService)
        {
            _contactsRepository = contactsRepository;
            _uploadService = uploadService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var contacts = _contactsRepository.GetAll();

            if (contacts.Count > 0)
            {
                return Ok(new { Contacts = contacts });
            }
            else
            {
                return NotFound(new { Message = "Le répertoire est vide !" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var contact = _contactsRepository.GetById(id);

            if (contact == null) return NotFound(new { Message = "Aucun contact ne possède cet ID !" });
            else
            {
                return Ok(new { Contact = contact });
            }
        }

        [HttpPost]
        public IActionResult Create([FromForm] string firstname, [FromForm] string lastname, [FromForm] string email, [FromForm] string password, [FromForm] string phone, IFormFile avatar)
        {
            Contact contact = new Contact()
            {
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                Password = password,
                Phone = phone,
                AvatarURL = _uploadService.Upload(avatar)
            };

            Contact newContact = _contactsRepository.Add(contact);
            if (newContact == null) return BadRequest(new { Message = "Une erreur s'est produite, aucun contact n'a été ajouté..." });
            else return Ok(new { Message = $"{newContact.Fullname} ajouté avec succès !", ContactID = newContact.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromForm] string firstname, [FromForm] string lastname, [FromForm] string email, [FromForm] string password, [FromForm] string phone, IFormFile avatar)
        {
            var contact = _contactsRepository.GetById(id);

            if (contact == null) return NotFound(new { Message = "Aucun contact ne possède cet ID !" });
            else
            {
                Contact newContact = new Contact()
                {
                    Firstname = firstname,
                    Lastname = lastname,
                    Email = email,
                    Password = password,
                    Phone = phone,
                    AvatarURL = _uploadService.Upload(avatar)
                };

                Contact editedContact = _contactsRepository.Update(id, newContact);
                if (editedContact == null) return BadRequest(new { Message = "Une erreur s'est produite, aucun contact n'a été modifié..." });
                else return Ok(new { Message = $"{newContact.Fullname} édité avec succès !", ContactID = newContact.Id, newContact = editedContact });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Contact oldContact = _contactsRepository.GetById(id);

            if (oldContact == null) return NotFound(new { Message = "Aucun contact ne possède cet ID !" });
            else
            {
                if (_contactsRepository.Delete(id))
                {
                    return Ok(new { Message = $"{oldContact.Fullname} supprimé avec succès" });
                }
                else
                {
                    return NotFound(new { Message = "Un problème est survenu, la suppression n'a pas eu lieu..." });
                }
            }
        }
    }
}
