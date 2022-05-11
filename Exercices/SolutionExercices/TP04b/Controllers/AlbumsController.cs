using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP04b.Models;
using TP04b.Services;

namespace TP04b.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AlbumsController : ControllerBase
    {
        private readonly FakeDbService context;

        public AlbumsController(FakeDbService context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult AddAlbum(Album newAlbum)
        {
            newAlbum.Id = ++Album.Count;

            context.Albums.Add(newAlbum);

            return Ok(new
            {
                Message = "Album ajouté avec succès en base de données !",
                Album = newAlbum
            });
        }

        [AllowAnonymous]
        [HttpGet("/albums")]
        public IActionResult GetAllAlbums()
        {
            var albums = context.Albums;

            if (albums.Count > 0)
            {
                return Ok(new
                {
                    Message = "Voici la liste des albums :",
                    Albums = albums
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a pas d'album dans la base de données !"
            });

        }

        [AllowAnonymous]
        [HttpGet("/album/{id}")]
        public IActionResult GetAlbumById(int id)
        {
            var album = context.Albums.FirstOrDefault(x => x.Id == id);

            if (album != null)
            {
                return Ok(new
                {
                    Message = "Voici l'album demandé :",
                    Album = album
                });
            }

            return NotFound(new
            {
                Message = "Il n'y a pas d'album avec cet Id !"
            });

        }

        [HttpPatch("/album/{id}")]
        public IActionResult EditAlbumById(int id, Album newValues)
        {
            var found = context.Albums.FirstOrDefault(x => x.Id == id);

            if (found != null)
            {
                found.Title = newValues.Title;
                found.Artist = newValues.Artist;
                found.Price = newValues.Price;
                found.CoverURL = newValues.CoverURL;

                return Ok(new
                {
                    Message = "Album modifié avec succès ! ",
                    Album = found
                });
            }

            return NotFound(new
            {
                Message = "Aucun album n'a été trouvé en base de données avec cet Id !"
            });

        }

        [HttpDelete("/album/{id}")]
        public IActionResult DeleteAlbumById(int id)
        {
            var found = context.Albums.FirstOrDefault(x => x.Id == id);

            if (found != null)
            {
                context.Albums.Remove(found);

                return Ok(new
                {
                    Message = "Album supprimé avec succès ! "
                });
            }

            return NotFound(new
            {
                Message = "Aucun album n'a été trouvé en base de données avec cet Id !"
            });
        }
    }
}
