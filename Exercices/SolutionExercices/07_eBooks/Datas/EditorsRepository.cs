using _07_eBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace _07_eBooks.Datas
{
    public class EditorsRepository : BaseRepository, IRepository<Editor>
    {
        public EditorsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Editor Add(Editor entity)
        {
            context.Editors.Add(entity);

            if (context.SaveChanges() > 0) return entity;

            return null;
        }

        public bool Delete(int id)
        {
            var editor = GetById(id);

            if (editor != null)
            {
                context.Editors.Remove(editor);

                if (context.SaveChanges() > 0) return true;
            }

            return false;
        }

        public ICollection<Editor> GetAll()
        {
            return context.Editors.Include(x => x.Books).ToList();
        }

        public Editor GetById(int id)
        {
            return context.Editors.Include(x => x.Books).FirstOrDefault(x => x.Id == id);
        }

        public Editor Update(int id, Editor entity)
        {
            var editor = GetById(id);

            if (editor != null)
            {
                editor.FirstName = entity.FirstName;
                editor.LastName = entity.LastName;
                editor.DateOfBirth = entity.DateOfBirth;
                editor.Gender = entity.Gender;
                
                if (context.SaveChanges() > 0) return editor;
            }

            return null;
        }
    }
}
