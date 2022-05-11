using DemoAPI_EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI_EFCore.Datas
{
    public class DogsRepository : BaseRepository, IRepository<Dog>
    {
        public DogsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Dog Add(Dog entity)
        {
            context.Dogs.Add(entity);

            if (context.SaveChanges() > 0) return entity;

            return null;
        }

        public bool Delete(int id)
        {
            var found = GetById(id);

            if (found != null)
            {
                context.Dogs.Remove(found);
            }

            return context.SaveChanges() > 0;
        }

        public ICollection<Dog> GetAll()
        {
            return context.Dogs.Include(x => x.Master).ThenInclude(x => x.Address).ToList();
        }

        public Dog GetById(int id)
        {
            return context.Dogs.Include(x => x.Master).ThenInclude(x => x.Address).FirstOrDefault(x => x.Id == id);
        }

        public Dog Update(int id, Dog entity)
        {
            var found = GetById(id);

            if (found != null)
            {
                found.Name = entity.Name;
                found.Breed = entity.Breed;
                found.MasterId = entity.MasterId;

                if (context.SaveChanges() > 0) return found;
            }

            return null;
        }
    }
}
