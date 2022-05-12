using _07_eBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace _07_eBooks.Datas
{
    public class AuthorsRepository : BaseRepository, IRepository<Author>
    {
        public AuthorsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Author Add(Author entity)
        {
            context.Authors.Add(entity);

            if (context.SaveChanges() > 0) return entity;

            return null;
        }

        public bool Delete(int id)
        {
            var author = GetById(id);

            if (author != null)
            {
                context.Authors.Remove(author);
            }

            return context.SaveChanges() > 0;
        }

        public ICollection<Author> GetAll()
        {
            return context.Authors.Include(x => x.Books).ToList();
        }

        public Author GetById(int id)
        {
            return context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == id);
        }

        public Author Update(int id, Author entity)
        {
            var author = GetById(id);

            if (author != null)
            {
                author.FirstName = entity.FirstName;
                author.LastName = entity.LastName;
                author.DateOfBirth = entity.DateOfBirth;
                author.Gender = entity.Gender;

                if (context.SaveChanges() > 0) return author;
            }

            return null;
        }
    }
}
