using _03_Demenagements.Models;
using Microsoft.EntityFrameworkCore;

namespace _03_Demenagements.Datas
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Person> Persons { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
