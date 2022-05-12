using GharrowDogsAPI.Datas;
using Microsoft.EntityFrameworkCore;

namespace GharrowDogsAPI.Services
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
