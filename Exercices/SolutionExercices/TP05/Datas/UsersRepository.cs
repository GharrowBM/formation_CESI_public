using TP05.Models;

namespace TP05.Datas
{
    public class UsersRepository : BaseRepository, IRepository<User>
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public User Add(User entity)
        {
            _context.Users.Add(entity);
            if (_context.SaveChanges() > 0)
            {
                return entity;
            }

            return null;
        }

        public bool Delete(int id)
        {
            _context.Users.Remove(GetById(id));
            return _context.SaveChanges() > 0;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Update(int id, User entity)
        {
            User userToEdit = GetById(id);

            if (userToEdit != null)
            {
                userToEdit.Firstname = entity.Firstname;
                userToEdit.Lastname = entity.Lastname;
                userToEdit.Email = entity.Email;
                userToEdit.Phone = entity.Phone;
                userToEdit.Password = entity.Password;

                _context.SaveChanges();
            }

            return userToEdit;
        }
    }
}
