using _06_Librarie.Models;

namespace _06_Librarie.Datas
{
    public class BooksRepository : BaseRepository, IRepository<Book>
    {
        public BooksRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Book Add(Book entity)
        {
            context.Books.Add(entity);

            if (context.SaveChanges() > 0)
            {
                return entity;
            }

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
            return context.Books.ToList();
        }

        public Book GetById(int id)
        {
            return context.Books.FirstOrDefault(x => x.Id == id);
        }

        public Book Update(int id, Book entity)
        {
            var book = GetById(id);

            if (book != null)
            {
                book.Title = entity.Title;
                book.ISBN = entity.ISBN;
                book.Author = entity.Author;
                book.Editor = entity.Editor;
                book.Price = entity.Price;
                book.Quantity = entity.Quantity;
                book.ReleaseDate = entity.ReleaseDate;
                book.Score = entity.Score;

                if (context.SaveChanges() > 0)
                {
                    return entity;
                }
            }

            return null;
        }
    }
}
