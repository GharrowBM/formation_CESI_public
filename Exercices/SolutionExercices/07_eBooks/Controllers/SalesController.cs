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
    public class SalesController : ControllerBase
    {
        private readonly IRepository<Book> booksRepository;
        private readonly IRepository<Sale> salesRepository;

        public SalesController(IRepository<Book> booksRepository, IRepository<Sale> salesRepository)
        {
            this.booksRepository = booksRepository;
            this.salesRepository = salesRepository;
        }

        [HttpPost("/sale")]
        [Authorize]
        public IActionResult AddSale(Sale newSale)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var addedSale = salesRepository.Add(newSale);

            if (addedSale != null)
            {
                return Ok(new
                {
                    Message = "La vente a été ajoutée en base de données avec succès !",
                    Sale = addedSale
                });
            }

            return BadRequest(new
            {
                Message = "Un problème est survenu lors de l'ajout de la vente en base de données !"
            });
        }

        [HttpGet("/sales")]
        public IActionResult GetAllSales()
        {
            var sales = salesRepository.GetAll();

            if (sales.Count > 0)
            {
                return Ok(new
                {
                    Message = "Voici les ventes présentes en base de données :",
                    Sales = sales
                });
            }

            return NotFound(new
            {
                Message = "Il n'y actuellement aucune vente en base de données !"
            });
        }

        [HttpGet("/sale/{id}")]
        public IActionResult GetSaleById(int id)
        {
            var sale = salesRepository.GetById(id);

            if (sale != null)
            {
                return Ok(new
                {
                    Message = "Voici la vente demandée :",
                    Sale = sale
                });
            }

            return NotFound(new
            {
                Message = "Cette vente n'existe pas en base de données !"
            });
        }

        [HttpPatch("/sale/{id}")]
        public IActionResult EditSale(int id, Sale newValues)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var sale = salesRepository.GetById(id);

            if (sale != null)
            {
                var editedSale = salesRepository.Update(id, newValues);

                if (editedSale != null)
                {
                    return Ok(new
                    {
                        Message = "La vente a été correctement modifiée",
                        Sale = editedSale
                    });
                }

                return BadRequest(new
                {
                    Message = "Un problème est survenu lors de la modification de la vente en base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Cette vente n'existe pas en base de données !"
            });
        }

        [HttpDelete("/sale/{id}")]
        public IActionResult DeleteSale(int id)
        {
            var sale = salesRepository.GetById(id);

            if (sale != null)
            {
                if (salesRepository.Delete(id))
                {
                    return Ok(new
                    {
                        Message = "La vente a été correctement supprimée !"
                    });
                }

                return BadRequest(new
                {
                    Message = "Un problème est survenu lors de la suppression de la vente en base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Cette vente n'existe pas en base de données !"
            });
        }


    }
}
