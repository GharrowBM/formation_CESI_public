using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP05.Datas;
using TP05.Models;

namespace TP05.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("allConnections")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private IRepository<Pizza> _pizzasRepository;

        public PizzasController(IRepository<Pizza> pizzasRepository)
        {
            _pizzasRepository = pizzasRepository;
        }

        [HttpGet("/menu")]
        [Authorize("user")]
        public IActionResult GetMenu()
        {
            var pizzas = _pizzasRepository.GetAll();

            return Ok(new {Pizzas = pizzas});
        }

        [HttpPost("/pizza")]
        [Authorize("admin")]
        public IActionResult SubmitNewPizza([FromForm] string name, [FromForm] string description, [FromForm] double price, [FromForm] bool isVegan, [FromForm] bool isSpicy, [FromForm] string pictureURL)
        {
            Pizza newPizza = new Pizza()
            {
                Name = name,
                Description = description,
                Price = price,
                IsVegan = isVegan,
                IsSpicy = isSpicy,
                PictureURL = pictureURL
            };

            if (_pizzasRepository.Add(newPizza) != null)
            {
                return Ok(new { Message = $"{newPizza.Name} ajoutée au menu avec succès !" });
            }
            else return BadRequest(new { Message = "Ajout impossible de la pizza !" });
        }

        [HttpPost("/pizza/add-topping/{pizzaId}")]
        [Authorize("admin")]
        public IActionResult AddIngredientToPizza(int pizzaId, [FromForm] string name, [FromForm] string description)
        {
            Pizza pizzaToEdit = _pizzasRepository.GetById(pizzaId);

            if (pizzaToEdit == null)
            {
                return NotFound(new { Message = "Pas de pizza avec cet ID !" });
            }
            else
            {
                pizzaToEdit.Ingredients.Add(new Ingredient()
                {
                    Name = name,
                    Description = description
                });

                _pizzasRepository.Update(pizzaId, pizzaToEdit);

                return Ok(new { Message = "Pizza modifiée avec succès", newPizza = pizzaToEdit });
            }
        }

        [HttpDelete("/pizza/remove-topping/{pizzaId}/{toppingId}")]
        [Authorize("admin")]
        public IActionResult RemoveIngredientFromPizza(int pizzaId, int toppingId)
        {
            Pizza pizzaToEdit = _pizzasRepository.GetById(pizzaId);

            if (pizzaToEdit == null)
            {
                return NotFound(new { Message = "Pas de pizza avec cet ID !" });
            }
            else
            {
                pizzaToEdit.Ingredients.Remove(pizzaToEdit.Ingredients.Find(ingredient => ingredient.Id == toppingId));

                _pizzasRepository.Update(pizzaId, pizzaToEdit);

                return Ok(new { Message = "Pizza modifiée avec succès", newPizza = pizzaToEdit });
            }
        }

        [HttpPut("/pizza/{id}")]
        [Authorize("admin")]
        public IActionResult EditPizza(int id, [FromForm] string name, [FromForm] string description, [FromForm] double price, [FromForm] bool isVegan, [FromForm] bool isSpicy, [FromForm] string pictureURL)
        {
            Pizza pizzaToEdit = _pizzasRepository.GetById(id);

            if (pizzaToEdit == null) return NotFound(new { Message = "Pizza introuvable ! Avez vous le bon ID ?" });
            else
            {
                Pizza newPizza = new Pizza()
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    IsVegan = isVegan,
                    IsSpicy = isSpicy,
                    PictureURL = pictureURL,
                    Ingredients = pizzaToEdit.Ingredients,
                };

                if (_pizzasRepository.Update(id, newPizza) != null)
                {
                    return Ok(new { Message = $"{newPizza.Name} modifiée avec succès !", NewPizza = newPizza });
                }
                else return BadRequest(new { Message = "Modification impossible de la pizza !" });
            }
        }

        [HttpDelete("/pizza/{id}")]
        [Authorize("admin")]
        public IActionResult DeletePizza(int id)
        {
            Pizza pizzaToDelete = _pizzasRepository.GetById(id);

            if (pizzaToDelete == null) return NotFound(new { Message = "Pizza introuvable ! Avez vous le bon ID ?" });
            else
            {
                if (_pizzasRepository.Delete(id))
                {
                    return Ok(new { Message = $"{pizzaToDelete.Name} supprimée avec succès !" });
                }
                else
                {
                    return BadRequest(new { Message = "Suppression de la pizza impossible !" });
                }
            }
        }
    }
}
