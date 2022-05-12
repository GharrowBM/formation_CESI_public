using GharrowDogsAPI.Datas;
using GharrowDogsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GharrowDogsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DogsController : ControllerBase
    {
        private readonly IRepository<Dog> dogsRepository;

        public DogsController(IRepository<Dog> dogsRepository)
        {
            this.dogsRepository = dogsRepository;
        }

        [HttpPost("/dog")]
        public IActionResult AddDog(Dog newDog)
        {

            dogsRepository.Add(newDog);

            return Ok(new
            {
                Message = "Chien ajouté avec succès en base de données !",
                Dog = newDog
            });
        }

        [HttpGet("/dogs")]
        [AllowAnonymous]
        public IActionResult GetAllDogs()
        {
            var dogs = dogsRepository.GetAll();

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
        [AllowAnonymous]
        public IActionResult GetDogById(int id)
        {
            var dog = dogsRepository.GetById(id);

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
            var found = dogsRepository.GetById(id);

            if (found == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas de chien avec cet Id !"
                });
            }

            dogsRepository.Update(id, newValues);

            return Ok(new
            {
                Message = "Chien modifié avec succès !",
                Dog = found
            });
        }

        [HttpDelete("/dog/{id}")]
        public IActionResult DeleteDogById(int id)
        {
            var found = dogsRepository.GetById(id);

            if (found == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas de chien avec cet Id !"
                });
            }

            dogsRepository.Delete(id);

            return Ok(new
            {
                Message = "Chien supprimé avec succès !"
            });
        }
    }
}
