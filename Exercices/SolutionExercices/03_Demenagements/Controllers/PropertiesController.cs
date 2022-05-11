using _03_Demenagements.Datas;
using _03_Demenagements.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _03_Demenagements.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PropertiesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("/property")]
        public IActionResult AddProperty(Property newProperty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Properties.Add(newProperty);

            if (context.SaveChanges() > 0)
            {
                return Ok(new
                {
                    Message = "La propriété a été ajoutée en base de données !",
                    Property = newProperty
                });
            }

            return BadRequest(new
            {
                Message = "Ajout impossible de la propriété en base de données !"
            });
        }

        [HttpGet("/properties")]
        public IActionResult GetAllProperties()
        {
            var properties = context.Properties.Include(x => x.Inhabitants).ToList();

            if (properties.Count > 0)
            {
                return Ok(new
                {
                    Message = "Voici la liste des propriétés :",
                    Properties = properties
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a pas de pripriétés en base de données !"
            });
        }

        [HttpGet("/property/{id}")]
        public IActionResult GetPropertyById(int id)
        {
            var property = context.Properties.Include(x => x.Inhabitants).SingleOrDefault(p => p.Id == id);

            if (property == null)
            {
                return NotFound(new
                {
                    Message = "Il n'y a pas de propriété avec cet Id dans la base de données !"
                });
            }

            return Ok(new
            {
                Message = "Voici la propriété demandée :",
                Property = property
            });
        }

        [HttpPatch("/property/{id}")]
        public IActionResult EditPropertyById(int id, Property newValues)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var found = context.Properties.FirstOrDefault(x => x.Id == id);

            if (found != null)
            {
                found.StreetNumber = newValues.StreetNumber;
                found.StreetName = newValues.StreetName;
                found.PostalCode = newValues.PostalCode;
                found.CityName = newValues.CityName;

                if (context.SaveChanges() > 0)
                {
                    return Ok(new
                    {
                        Message = "La propriété a été éditée avec succès !",
                        Property = found
                    });

                }

                return BadRequest(new
                {
                    Message = "Edition impossible de la propriété dans la base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a pas de propriété en base de données avec cet Id !"
            });

        }

        [HttpDelete("/property/{id}")]
        public IActionResult DeletePropertyById(int id)
        {
            var found = context.Properties.FirstOrDefault(x => x.Id == id);

            if (found != null)
            {
                context.Properties.Remove(found);

                if (context.SaveChanges() > 0)
                {
                    return Ok(new
                    {
                        Message = "La propriété a été supprimée avec succès !"
                    });

                }

                return BadRequest(new
                {
                    Message = "Suppression impossible de la propriété dans la base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a pas de propriété en base de données avec cet Id !"
            });
        }

        [HttpGet("/move-in/{personId}/{propertyId}")]
        public IActionResult MakeMoveIn(int personId, int propertyId)
        {
            var personFound = context.Persons.Include(x => x.Properties).FirstOrDefault(x => x.Id == personId);
            var propertyFound = context.Properties.FirstOrDefault(x => x.Id == propertyId);

            if (personFound != null)
            {
                if (propertyFound != null)
                {
                    if (!personFound.Properties.Contains(propertyFound))
                    {
                        personFound.Properties.Add(propertyFound);

                        if (context.SaveChanges() > 0)
                        {
                            return Ok(new
                            {
                                Message = "L'emménagement a bien été effectué en base de données !"
                            });
                        }

                        return BadRequest(new
                        {
                            Message = "Emménagement impossible à ajouter en base de donnée !"
                        });
                    }

                    return BadRequest(new
                    {
                        Message = "La personne habite déjà à cette adresse !"
                    });
                }

                return NotFound(new
                {
                    Message = "Il n'y a pas de propriété en base de données possédant cet Id !"
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a personne dans la base de donnée possédant cet Id !"
            });
        }

        [HttpGet("/move-out/{personId}/{propertyId}")]
        public IActionResult MakeMoveOut(int personId, int propertyId)
        {
            var personFound = context.Persons.Include(x => x.Properties).FirstOrDefault(x => x.Id == personId);
            var propertyFound = context.Properties.FirstOrDefault(x => x.Id == propertyId);

            if (personFound != null)
            {
                if (propertyFound != null)
                {
                    if (personFound.Properties.Contains(propertyFound))
                    {
                        personFound.Properties.Remove(propertyFound);

                        if (context.SaveChanges() > 0)
                        {
                            return Ok(new
                            {
                                Message = "Le déménagement a bien été effectué en base de données !"
                            });
                        }

                        return BadRequest(new
                        {
                            Message = "Déménagement impossible à ajouter en base de donnée !"
                        });
                    }

                    return BadRequest(new
                    {
                        Message = "La personne n'habite pas à cette adresse !"
                    });
                }

                return NotFound(new
                {
                    Message = "Il n'y a pas de propriété en base de données possédant cet Id !"
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a personne dans la base de donnée possédant cet Id !"
            });
        }
    }
}
