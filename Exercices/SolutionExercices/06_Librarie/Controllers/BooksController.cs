using _06_Librarie.Datas;
using _06_Librarie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _06_Librarie.Controllers
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
        public IActionResult AddBook(Book newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedBook = booksRepository.Add(newBook);

            if (addedBook != null)
            {
                return Ok(new
                {
                    Message = "Livre ajouté avec succès !",
                    Book = addedBook
                });
            }

            return BadRequest(new
            {
                Message = "Un problème est survenu lors de l'ajout du livre !"
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
                    Message = "Voici la liste des ouvrages en base de données :",
                    Books = books
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a aucun ouvrage en base de données !"
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
                    Message = "Voici l'ouvrage demandé :",
                    Book = book
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a aucun ouvrage en base de données !"
            });
        }

        [HttpPatch("/book/{id}")]
        public IActionResult EditBook(int id, Book newValues)
        {
            var book = booksRepository.GetById(id);

            if (book != null)
            {
                var editedBook = booksRepository.Update(id, newValues);

                if (editedBook != null)
                {
                    return Ok(new
                    {
                        Message = "Livre édité avec succès !",
                        Book = editedBook
                    });
                }

                return BadRequest(new
                {
                    Message = "Un problème est survenu lors de l'édition du livre en base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a aucun ouvrage en base de données !"
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
                        Message = "Livre supprimé avec succès !"
                    });
                }

                return BadRequest(new
                {
                    Message = "Un problème est survenu lors de la suppression  du livre en base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a aucun ouvrage en base de données !"
            });
        }
    }
}
