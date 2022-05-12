using _07_eBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace _07_eBooks.Datas
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Editor> Editors { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
