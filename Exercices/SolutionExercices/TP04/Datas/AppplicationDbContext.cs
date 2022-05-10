using Microsoft.EntityFrameworkCore;
using TP04.Models;

namespace TP04.Datas
{
    public class AppplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Contact> Contacts { get; set; }

        public AppplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionString:Default"]);
        }
    }
}
