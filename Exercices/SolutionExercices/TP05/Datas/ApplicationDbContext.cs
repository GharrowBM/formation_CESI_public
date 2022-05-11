using Microsoft.EntityFrameworkCore;
using TP05.Models;

namespace TP05.Datas
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set;}
        public DbSet<Pizza> Pizzas { get; set; }

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionString:Default"]);
        }
    }
}
