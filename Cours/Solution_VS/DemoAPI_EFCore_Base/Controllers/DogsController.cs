using DemoAPI_EFCore.Datas;
using DemoAPI_EFCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI_EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DogsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DogsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("/dog")]
        public IActionResult AddDog(Dog newDog)
        {

            context.Dogs.Add(newDog);

            return Ok(new
            {
                Message = "Chien ajouté avec succès en base de données !",
                Dog = newDog
            });
        }

        [AllowAnonymous]
        [HttpGet("/dogs")]
        public IActionResult GetAllDogs()
        {
            var dogs = context.Dogs.Include(x => x.Master).ThenInclude(x => x.Address).ToList();

            if (dogs.Count == 0)
                return NotFound(new
                {
                    Message = "Il n'y a pas de chien dans la base de données !"
                });

            return Ok(new
            {
                Message = "Voici les chiens présents en base de données :",
                Dogs = dogs
            });
        }

        [HttpGet("/dog/{id}")]
        public IActionResult GetDogById(int id)
        {
            var dog = context.Dogs.Include(x => x.Master).ThenInclude(x => x.Address).FirstOrDefault(x => x.Id == id);

            if (dog == null)
                return NotFound(new
                {
                    Message = "Il n' y a pas de chien avec cet Id !"
                });

            return Ok(new
            {
                Message = "Voici le chien demandé :",
                Dog = dog
            });
        }

        [HttpPatch("/dog/{id}")]
        public IActionResult EditDogById(int id, Dog newValues)
        {
            var found = context.Dogs.Include(x => x.Master).ThenInclude(x => x.Address).FirstOrDefault(x => x.Id == id);

            if (found == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas de chien avec cet Id !"
                });
            }

            found.Name = newValues.Name;
            found.Breed = newValues.Breed;
            found.MasterId = newValues.MasterId;

            if (context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "Chien modifié avec succès !",
                    Dog = found
                });
            }

            return BadRequest(new
            {
                Message = "Un problème a eu lieu lors de l'édition en base de données !"
            });

        }

        [HttpDelete("/dog/{id}")]
        public IActionResult DeleteDogById(int id)
        {
            var found = context.Dogs.Include(x => x.Master).ThenInclude(x => x.Address).FirstOrDefault(x => x.Id == id);

            if (found == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas de chien avec cet Id !"
                });
            }

            context.Dogs.Remove(found);

            if (context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "Chien supprimé avec succès !"
                });
            }

            return BadRequest(new
            {
                Message = "Il y eu un problème lors de la suppression en base de données !"
            });

        }
    }
}
