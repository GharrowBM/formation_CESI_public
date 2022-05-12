using _07_eBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace _07_eBooks.Datas
{
    public class SalesRepository : BaseRepository, IRepository<Sale>
    {
        public SalesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Sale Add(Sale entity)
        {
            context.Sales.Add(entity);

            if (context.SaveChanges() > 0) return entity;

            return null;
        }

        public bool Delete(int id)
        {
            var sale = GetById(id);

            if (sale != null)
            {
                context.Sales.Remove(sale);
            }

            return context.SaveChanges() > 0;
        }

        public ICollection<Sale> GetAll()
        {
            return context.Sales.Include(x => x.Books).ToList();
        }

        public Sale GetById(int id)
        {
            return context.Sales.Include(x => x.Books).FirstOrDefault(x => x.Id == id);
        }

        public Sale Update(int id, Sale entity)
        {
            var sale = GetById(id);

            if (sale != null)
            {
                sale.DateOfSale = entity.DateOfSale;
                sale.TotalValue = entity.TotalValue;
                sale.Books = entity.Books;

                if (context.SaveChanges() > 0)
                {
                    return sale;
                }
            }

            return null;
        }
    }
}
