using _06_Librarie.Models;
using Microsoft.EntityFrameworkCore;

namespace _06_Librarie.Datas
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Book> Books { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
