using GharrowDogsAPI.Models;

namespace GharrowDogsAPI.Datas
{
    public class AccountsRepository : BaseRepository, IRepository<Account>
    {
        public AccountsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Account Add(Account entity)
        {
            context.Accounts.Add(entity);

            if (context.SaveChanges() > 0) return entity;

            return null;
        }

        public bool Delete(int id)
        {
            var found = GetById(id);

            if (found != null)
            {
                context.Accounts.Remove(found);
            }

            return context.SaveChanges() > 0;
        }

        public ICollection<Account> GetAll()
        {
            return context.Accounts.ToList();
        }

        public Account GetById(int id)
        {
            return context.Accounts.FirstOrDefault(x => x.Id == id);
        }

        public Account Update(int id, Account entity)
        {
            var found = GetById(id);

            if (found != null)
            {
                found.UserName = entity.UserName;
                found.Password = entity.Password;
                found.EmailAddress = entity.EmailAddress;
                found.IsAdmin = entity.IsAdmin;

                if (context.SaveChanges() > 0) return found;
            }

            return null;
        }
    }
}
