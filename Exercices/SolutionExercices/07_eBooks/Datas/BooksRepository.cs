using _07_eBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace _07_eBooks.Datas
{
    public class BooksRepository : BaseRepository, IRepository<Book>
    {
        public BooksRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Book Add(Book entity)
        {
            context.Books.Add(entity);

            if (context.SaveChanges() > 0) return entity;

            return null;
        }

        public bool Delete(int id)
        {
            var book = GetById(id);

            if (book != null)
            {
                context.Books.Remove(book);
            }

            return context.SaveChanges() > 0;
        }

        public ICollection<Book> GetAll()
        {
            return context.Books.Include(x => x.Author).Include(x => x.Editor).ToList();
        }

        public Book GetById(int id)
        {
            return context.Books.Include(x => x.Author).Include(x => x.Editor).FirstOrDefault(x => x.Id == id);
        }

        public Book Update(int id, Book entity)
        {
            var book = GetById(id);

            if (book != null)
            {
                book.Title = entity.Title;
                book.ISBN = entity.ISBN;
                book.CoverURL = entity.CoverURL;
                book.ReleaseDate = entity.ReleaseDate;
                book.NbOfPages = entity.NbOfPages;
                book.AuthorId = entity.AuthorId;
                book.EditorId = entity.EditorId;
                
                if (context.SaveChanges() > 0)
                {
                    return book;
                }
            }

            return null;
        }
    }
}
