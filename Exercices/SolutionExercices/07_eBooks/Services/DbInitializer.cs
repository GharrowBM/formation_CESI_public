using _07_eBooks.Datas;
using Microsoft.EntityFrameworkCore;

namespace _07_eBooks.Services
{
    public static class DbInitializer
    {
        public static void InitMigration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                scope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
            }
        }
    }
}
