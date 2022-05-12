using GharrowDogsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GharrowDogsAPI.Datas
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
