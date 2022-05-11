using DemoAPI_EFCore.Models;
using DemoAPI_EFCore.Datas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI_EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AddressesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AddressesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("/address")]
        public IActionResult AddAddress(Address newAddress)
        {

            context.Addresses.Add(newAddress);

            return Ok(new
            {
                Message = "Adresse ajoutée avec succès en base de données !",
                Address = newAddress
            });
        }

        [AllowAnonymous]
        [HttpGet("/addresses")]
        public IActionResult GetAllAddresses()
        {
            var addresses = context.Addresses.ToList(); 

            if (addresses.Count == 0) 
                return NotFound(new
                {
                    Message = "Il n'y a pas d'adresses dans la base de données !"
                });

            return Ok(new
            {
                Message = "Voici les adresses présentes en base de données :",
                Addresses = addresses
            });
        }

        [HttpGet("/address/{id}")]
        public IActionResult GetAddressById(int id)
        {
            var address = context.Addresses.FirstOrDefault(x => x.Id == id);

            if (address == null)
                return NotFound(new
                {
                    Message = "Il n' y a pas d'adresse avec cet Id !"
                });

            return Ok(new
            {
                Message = "Voici l'adresse demandée :",
                Address = address
            });
        }

        [HttpPatch("/address/{id}")]
        public IActionResult EditAddressById(int id, Address newValues)
        {
            var found = context.Addresses.FirstOrDefault(x => x.Id == id);

            if (found == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas d'adresse avec cet Id !"
                });
            }

            found.StreetNumber = newValues.StreetNumber;
            found.StreetName = newValues.StreetName;
            found.PostalCode = newValues.PostalCode;
            found.CityName = newValues.CityName;

            if (context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "Adresse modifiée avec succès !",
                    Address = found
                });
            }

            return BadRequest(new
            {
                Message = "Il y a eu un problème lors de l'édition en base de donnée !"
            });

        }

        [HttpDelete("/address/{id}")]
        public IActionResult DeleteAddressById(int id)
        {
            var found = context.Addresses.FirstOrDefault(x => x.Id == id);

            if (found == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas d'adresse avec cet Id !"
                });
            }

            context.Addresses.Remove(found);

            if (context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "Adresse supprimée avec succès !"
                });
            }

            return BadRequest(new
            {
                Message = "Il y a eu un problème lors de la suppression en base de données"
            });

        }
    }
}
