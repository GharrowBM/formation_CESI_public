using _07_eBooks.Datas;
using _07_eBooks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _07_eBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book> booksRepository;

        public BooksController(IRepository<Book> booksRepository)
        {
            this.booksRepository = booksRepository;
        }

        [HttpPost("/book")]
        public IActionResult AddBook(Book NewBook)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var addedBook = booksRepository.Add(NewBook);

            if (addedBook != null)
            {
                return Ok(new
                {
                    Message = "Le livre  a été ajouté en base de données avec succès !",
                    Book = addedBook
                });
            }

            return BadRequest(new
            {
                Message = "Un problème est survenu lors de l'ajout du livre en base de données !"
            });
        }

        [HttpGet("/books")]
        [AllowAnonymous]
        public IActionResult GetAllBooks()
        {
            var books = booksRepository.GetAll();

            if (books.Count > 0)
            {
                return Ok(new
                {
                    Message = "Voici les livres présents en base de données :",
                    Books = books
                });
            }

            return NotFound(new
            {
                Message = "Il n'y actuellement aucun livre en base de données !"
            });
        }

        [HttpGet("/book/{id}")]
        [AllowAnonymous]
        public IActionResult GetBookById(int id)
        {
            var book = booksRepository.GetById(id);

            if (book != null)
            {
                return Ok(new
                {
                    Message = "Voici le livre demandé :",
                    Book = book
                });
            }

            return NotFound(new
            {
                Message = "Ce livre n'existe pas en base de données !"
            });
        }

        [HttpPatch("/book/{id}")]
        public IActionResult EditBook(int id, Book newValues)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var book = booksRepository.GetById(id);

            if (book != null)
            {
                var editedBook = booksRepository.Update(id, newValues);

                if (editedBook != null)
                {
                    return Ok(new
                    {
                        Message = "Le livre a été correctement modifié",
                        Book = editedBook
                    });
                }

                return BadRequest(new
                {
                    Message = "Un problème est survenu lors de la modification du livre en base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Ce livre n'existe pas en base de données !"
            });
        }

        [HttpDelete("/book/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = booksRepository.GetById(id);

            if (book != null)
            {
                if (booksRepository.Delete(id))
                {
                    return Ok(new
                    {
                        Message = "Le livre a été correctement supprimée !"
                    });
                }

                return BadRequest(new
                {
                    Message = "Un problème est survenu lors de la suppression du livre en base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Ce livre n'existe pas en base de données !"
            });
        }
    }
}
