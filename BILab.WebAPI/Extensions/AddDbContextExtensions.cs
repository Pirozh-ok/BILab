using BILab.DataAccess;
using BILab.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BILab.Web.Extensions {
    public static class AddDbContextExtensions {
        public static void AddDbConnection(this IServiceCollection services, string connectionString) {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(connectionString);
            });
        }

        public static async Task SeedDataAsync(this IServiceCollection services) {
            var provider = services.BuildServiceProvider();
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var context = provider.GetRequiredService<ApplicationDbContext>();

            var seedData = new SeedData(userManager, context);
            await seedData.SeedUsers();
            //await seedData.SeedProcedures();
        }
    }
}
