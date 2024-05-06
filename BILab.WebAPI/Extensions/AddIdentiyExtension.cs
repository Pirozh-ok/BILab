using BILab.DataAccess;
using BILab.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace BILab.Web.Extensions {
    public static class AddIdentityExtensions {
        public static void AddIdentitySettings(this IServiceCollection services) {
            services.AddIdentity<User, Role>(opts => {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
