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
    public class MastersController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public MastersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("/master")]
        public IActionResult AddMaster(Master newMaster)
        {

            context.Masters.Add(newMaster);

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
            var masters = context.Masters.Include(x => x.Address).ToList();

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
            var master = context.Masters.Include(x => x.Address).FirstOrDefault(x => x.Id == id);

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
            var found = context.Masters.Include(x => x.Address).FirstOrDefault(x => x.Id == id);

            if (found == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas de maître avec cet Id !"
                });
            }

            found.FirstName = newValues.FirstName;
            found.LastName = newValues.LastName;
            found.Email = newValues.Email;
            found.PhoneNumber = newValues.PhoneNumber;
            found.DateOfBirth = newValues.DateOfBirth;
            found.AddressId = newValues.AddressId;

            if (context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "Maître édité avec succès !"
                });
            }

            return BadRequest(new
            {
                Message = "Edition impossible du maître en base de données !"
            });
        }

        [HttpDelete("/master/{id}")]
        public IActionResult DeleteMasterById(int id)
        {
            var found = context.Masters.Include(x => x.Address).FirstOrDefault(x => x.Id == id);

            if (found == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas de maître avec cet Id !"
                });
            }

            context.Masters.Remove(found);

            if (context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "Maître supprimé avec succès !"
                });
            }

            return BadRequest(new
            {
                Message = "Suppression impossible du maître en base de données !"
            });

        }
    }
}
