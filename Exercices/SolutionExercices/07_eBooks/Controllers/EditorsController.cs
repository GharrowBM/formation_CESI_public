using _07_eBooks.Datas;
using _07_eBooks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _07_eBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EditorsController : ControllerBase
    {
        private readonly IRepository<Editor> editorRepository;

        public EditorsController(IRepository<Editor> editorRepository)
        {
            this.editorRepository = editorRepository;
        }

        [HttpPost("/editor")]
        public IActionResult AddEditor(Editor newEditor)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var addedEditor = editorRepository.Add(newEditor);

            if (addedEditor != null)
            {
                return Ok(new
                {
                    Message = "L'éditeur a été ajouté en base de données avec succès !",
                    Editor = addedEditor
                });
            }

            return BadRequest(new
            {
                Message = "Un problème est survenu lors de l'ajout de l'éditeur en base de données !"
            });
        }

        [HttpGet("/editors")]
        [AllowAnonymous]
        public IActionResult GetAllEditors()
        {
            var editors = editorRepository.GetAll();

            if (editors.Count > 0)
            {
                return Ok(new
                {
                    Message = "Voici les éditeurs présents en base de données :",
                    Editors = editors
                });
            }

            return NotFound(new
            {
                Message = "Il n'y actuellement aucun éditeur en base de données !"
            });
        }

        [HttpGet("/editor/{id}")]
        [AllowAnonymous]
        public IActionResult GetEditorById(int id)
        {
            var editor = editorRepository.GetById(id);

            if (editor != null)
            {
                return Ok(new
                {
                    Message = "Voici l'éditeur demandé :",
                    Editor = editor
                });
            }

            return NotFound(new
            {
                Message = "Cet éditeur n'existe pas en base de données !"
            });
        }

        [HttpPatch("/editor/{id}")]
        public IActionResult EditEditor(int id, Editor newValues)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var editor = editorRepository.GetById(id);

            if (editor != null)
            {
                var editedEditor = editorRepository.Update(id, newValues);

                if (editedEditor != null)
                {
                    return Ok(new
                    {
                        Message = "L'éditeur a été correctement modifié",
                        Editor = editedEditor
                    });
                }

                return BadRequest(new
                {
                    Message = "Un problème est survenu lors de la modification de l'éditeur en base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Cet éditeur n'existe pas en base de données !"
            });
        }

        [HttpDelete("/editor/{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var editor = editorRepository.GetById(id);

            if (editor != null)
            {
                if (editorRepository.Delete(id))
                {
                    return Ok(new
                    {
                        Message = "L'éditeur a été correctement supprimée !"
                    });
                }

                return BadRequest(new
                {
                    Message = "Un problème est survenu lors de la suppression de l'éditeur en base de données !"
                });
            }

            return NotFound(new
            {
                Message = "Cet éditeur n'existe pas en base de données !"
            });
        }
    }
}
