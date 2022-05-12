using GharrowDogsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GharrowDogsAPI.Datas
{
    public class AddressesRepository : BaseRepository, IRepository<Address>
    {
        public AddressesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Address Add(Address entity)
        {
            context.Addresses.Add(entity);

            if (context.SaveChanges() > 0) return entity;

            return null;
        }

        public bool Delete(int id)
        {
            var found = GetById(id);

            if (found != null)
            {
                context.Addresses.Remove(found);
            }

            return context.SaveChanges() > 0;
        }

        public ICollection<Address> GetAll()
        {
            return context.Addresses.Include(x => x.Inhabitants).ThenInclude(x => x.Dogs).ToList();
        }

        public Address GetById(int id)
        {
            return context.Addresses.Include(x => x.Inhabitants).ThenInclude(x => x.Dogs).FirstOrDefault(x => x.Id == id);
        }

        public Address Update(int id, Address entity)
        {
            var found = GetById(id);

            if (found != null)
            {
                found.StreetNumber = entity.StreetNumber;
                found.StreetName = entity.StreetName;
                found.PostalCode = entity.PostalCode;
                found.CityName = entity.CityName;

                if (context.SaveChanges() > 0) return found;
            }

            return null;
        }
    }
}
