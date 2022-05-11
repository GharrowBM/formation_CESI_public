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
        private readonly IRepository<Address> addressesRepository;

        public AddressesController(IRepository<Address> addressesRepository)
        {
            this.addressesRepository = addressesRepository;
        }

        [HttpPost("/address")]
        public IActionResult AddAddress(Address newAddress)
        {

            addressesRepository.Add(newAddress);

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
            var addresses = addressesRepository.GetAll(); 

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
            var address = addressesRepository.GetById(id);

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
            var found = addressesRepository.GetById(id);

            if (found == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas d'adresse avec cet Id !"
                });
            }

            addressesRepository.Update(id, newValues);

            return Ok(new
            {
                Message = "Adresse modifiée avec succès !",
                Address = found
            });
        }

        [HttpDelete("/address/{id}")]
        public IActionResult DeleteAddressById(int id)
        {
            var found = addressesRepository.GetById(id);

            if (found == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas d'adresse avec cet Id !"
                });
            }

            addressesRepository.Delete(id);

            return Ok(new
            {
                Message = "Adresse supprimée avec succès !"
            });
        }
    }
}
