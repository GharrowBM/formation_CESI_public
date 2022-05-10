using TP04.Models;

namespace TP04.Datas
{
    public class ContactsRepository : BaseRepository, IRepository<Contact>
    {
        public ContactsRepository(AppplicationDbContext context) : base(context)
        {
        }

        public Contact Add(Contact entity)
        {
            _context.Contacts.Add(entity);
            if (_context.SaveChanges() > 0)
            {
                return entity;
            }

            return null;
        }

        public bool Delete(int id)
        {
            _context.Contacts.Remove(GetById(id));

            return _context.SaveChanges() > 0;
        }

        public List<Contact> GetAll()
        {
            return _context.Contacts.ToList();
        }

        public Contact GetById(int id)
        {
            return _context.Contacts.FirstOrDefault(x => x.Id == id);
        }

        public Contact GetByName(string name)
        {
            return _context.Contacts.FirstOrDefault(x => x.Lastname == name);
        }

        public List<Contact> FilterByName(string start)
        {
            var list = new List<Contact>();

            list.AddRange(_context.Contacts.Where(x => x.Fullname.StartsWith(start)).ToList());

            return list;
        }

        public Contact Update(int id, Contact entity)
        {
            Contact contactToEdit = GetById(id);

            if (contactToEdit != null)
            {
                contactToEdit.Firstname = entity.Firstname;
                contactToEdit.Lastname = entity.Lastname;
                contactToEdit.Email = entity.Email;
                contactToEdit.Phone = entity.Phone;
                contactToEdit.AvatarURL = entity.AvatarURL;
                contactToEdit.Password = entity.Password;

                _context.SaveChanges();
            }

            return contactToEdit;
        }
    }
}
