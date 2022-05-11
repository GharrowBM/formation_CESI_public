using DemoAPI_EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI_EFCore.Datas
{
    public class MastersRepository : BaseRepository, IRepository<Master>
    {
        public MastersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Master Add(Master entity)
        {
            context.Masters.Add(entity);

            if (context.SaveChanges() > 0) return entity;

            return null;
        }

        public bool Delete(int id)
        {
            var found = GetById(id);

            if (found != null)
            {
                context.Masters.Remove(found);
            }

            return context.SaveChanges() > 0;
        }

        public ICollection<Master> GetAll()
        {
            return context.Masters.Include(x => x.Address).ToList();
        }

        public Master GetById(int id)
        {
            return context.Masters.Include(x => x.Address).FirstOrDefault(x => x.Id == id);
        }

        public Master Update(int id, Master entity)
        {
            var found = GetById(id);

            if (found != null)
            {
                found.FirstName = entity.FirstName;
                found.LastName = entity.LastName;
                found.DateOfBirth = entity.DateOfBirth;
                found.PhoneNumber = entity.PhoneNumber;
                found.Email = entity.Email;
                found.AddressId = entity.AddressId;

                if (context.SaveChanges() > 0) return found;
            }

            return null;
        }
    }
}
