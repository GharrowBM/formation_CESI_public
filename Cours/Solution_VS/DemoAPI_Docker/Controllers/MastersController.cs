using DemoAPI_Docker.Datas;
using DemoAPI_Docker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI_Docker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MastersController : ControllerBase
    {
        private readonly IRepository<Master> mastersRepository;

        public MastersController(IRepository<Master> mastersRepository)
        {
            this.mastersRepository = mastersRepository;
        }

        [HttpPost("/master")]
        public IActionResult AddMaster(Master newMaster)
        {

            mastersRepository.Add(newMaster);

            return Ok(new
            {
                Message = "Maître ajouté avec succès en base de données !",
                Master = newMaster
            });
        }

        [AllowAnonymous]
        [HttpGet("/masters")]
        public IActionResult GetAllMasters()
        {
            var masters = mastersRepository.GetAll();

            if (masters.Count == 0)
                return NotFound(new
                {
                    Message = "Il n'y a pas de maître dans la base de données !"
                });

            return Ok(new
            {
                Message = "Voici les maîtres présents en base de données :",
                Masters = masters
            });
        }

        [HttpGet("/master/{id}")]
        public IActionResult GetMasterById(int id)
        {
            var master = mastersRepository.GetById(id);

            if (master == null)
                return NotFound(new
                {
                    Message = "Il n' y a pas de maître avec cet Id !"
                });

            return Ok(new
            {
                Message = "Voici le maître demandé :",
                Master = master
            });
        }

        [HttpPatch("/master/{id}")]
        public IActionResult EditMasterById(int id, Master newValues)
        {
            var found = mastersRepository.GetById(id);

            if (found == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas de maître avec cet Id !"
                });
            }

            mastersRepository.Update(id, newValues);

            return Ok(new
            {
                Message = "Maître modifié avec succès !",
                Master = found
            });
        }

        [HttpDelete("/master/{id}")]
        public IActionResult DeleteMasterById(int id)
        {
            var found = mastersRepository.GetById(id);

            if (found == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas de maître avec cet Id !"
                });
            }

            mastersRepository.Delete(id);

            return Ok(new
            {
                Message = "Maître supprimé avec succès !"
            });
        }
    }
}
