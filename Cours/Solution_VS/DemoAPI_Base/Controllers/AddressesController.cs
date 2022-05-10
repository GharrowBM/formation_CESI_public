using DemoAPI_Base.Models;
using DemoAPI_Base.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI_Base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly FakeDbService context;

        public AddressesController(FakeDbService context)
        {
            this.context = context;
        }

        [HttpPost("/address")]
        public IActionResult AddAddress(Address newAddress)
        {
            newAddress.Id = ++Address.Count;

            context.Addresses.Add(newAddress);

            return Ok(new
            {
                Message = "Adresse ajoutée avec succès en base de données !",
                Address = newAddress
            });
        }

        [HttpGet("/addresses")]
        public IActionResult GetAllAddresses()
        {
            var addresses = context.Addresses; 

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

            return Ok(new
            {
                Message = "Adresse modifiée avec succès !",
                Address = found
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

            return Ok(new
            {
                Message = "Adresse supprimée avec succès !"
            });
        }
    }
}
