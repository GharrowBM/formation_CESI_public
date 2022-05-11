using Microsoft.EntityFrameworkCore;
using TP05.Models;

namespace TP05.Datas
{
    public class PizzasRepository : BaseRepository, IRepository<Pizza>
    {
        public PizzasRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Pizza Add(Pizza entity)
        {
            _context.Pizzas.Add(entity);
            if (_context.SaveChanges() > 0)
            {
                return entity;
            }

            return null;
        }

        public bool Delete(int id)
        {
            _context.Pizzas.Remove(GetById(id));
            return _context.SaveChanges() > 0;
        }

        public List<Pizza> GetAll()
        {
            return _context.Pizzas.Include(pizza => pizza.Ingredients).ToList();
        }

        public Pizza GetById(int id)
        {
            return _context.Pizzas.Include(pizza => pizza.Ingredients).FirstOrDefault(pizza => pizza.Id == id);
        }

        public Pizza Update(int id, Pizza entity)
        {
            Pizza pizzaToEdit = GetById(id);

            if (pizzaToEdit != null)
            {
                pizzaToEdit.Name = entity.Name;
                pizzaToEdit.Description = entity.Description;
                pizzaToEdit.IsVegan = entity.IsVegan;
                pizzaToEdit.IsSpicy = entity.IsSpicy;
                pizzaToEdit.Price = entity.Price;
                pizzaToEdit.PictureURL = entity.PictureURL;
                pizzaToEdit.Ingredients = entity.Ingredients;

                _context.SaveChanges();
            }

            return pizzaToEdit;

        }
    }
}
