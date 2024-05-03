using BILab.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BILab.Web.Extensions {
    public static class AddDbContextExtensions {
        public static void AddDbConnection(this IServiceCollection services, string connectionString) {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
