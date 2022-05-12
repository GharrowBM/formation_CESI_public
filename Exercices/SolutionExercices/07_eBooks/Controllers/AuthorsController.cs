using _07_eBooks.Datas;
using _07_eBooks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _07_eBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AuthorsController : ControllerBase
    {
        private readonly IRepository<Author> authorRepository;

        public AuthorsController(IRepository<Author> authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        [HttpPost("/author")]
        public IActionResult AddAuthor(Author newAuthor)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var addedAuthor = authorRepository.Add(newAuthor);

            if (addedAuthor != null)
            {
                return Ok(new
                {
                    Message = "L'auteur a été ajouté en base de données avec succès !",
                    Author = addedAuthor
                });
            }

            return BadRequest(new
            {
                Message = "Un problème est survenu lors de l'ajout de l'auteur en base de données !"
            });
        }

        [HttpGet("/authors")]
        [AllowAnonymous]
        public IActionResult GetAllAuthors()
        {
            var authors = authorRepository.GetAll();

            if (authors.Count > 0)
            {
                return Ok(new
                {
                    Message = "Voici les auteurs présents en base de données :",
                    Authors = authors
                });
            }

            return NotFound(new
            {
                Message = "Il n'y actuellement aucun auteur en base de données !"
            });
        }

        [HttpGet("/author/{id}")]
        [AllowAnonymous]
        public IActionResult GetAuthorById(int id)
        {
            var author = authorRepository.GetById(id);

            if (author != null)
            {
                return Ok(new
                {
                    Message = "Voici l'auteur demandé :",
                    Author = author
                });
            }

            return NotFound(new
            {
                Message = "Cet auteur n'existe pas en base de données !"
            });
        }

        [HttpPatch("/author/{id}")]
        public IActionResult EditAuthor(int id, Author newValues)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var author = authorRepository.GetById(id);

            if (author != null)
            {
                var editedAuthor = authorRepository.Update(id, newValues);

                if (editedAuthor != null)
                {
                    return Ok(new
                    {
                        Message = "L'auteur a été correctement modifié",
                        Author = editedAuthor
                    });
                }

                return BadRequest(new
                {
                    Message = "Un problème est survenu lors de la modification de l'auteur en base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Cet auteur n'existe pas en base de données !"
            });
        }

        [HttpDelete("/author/{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = authorRepository.GetById(id);

            if (author != null)
            {
                if (authorRepository.Delete(id))
                {
                    return Ok(new
                    {
                        Message = "L'auteur a été correctement supprimée !"
                    });
                }

                return BadRequest(new
                {
                    Message = "Un problème est survenu lors de la suppression de l'auteur en base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Cet auteur n'existe pas en base de données !"
            });
        }
    }
}
