using BILab.Domain.Mapping;

namespace BILab.Web.Extensions {
    public static class AddMapperExtensions {
        public static void AddAutoMapper(this IServiceCollection services) {
            services.AddAutoMapper(
                typeof(AdressProfile),
                typeof(RoleProfile),
                typeof(SheduleProfile),
                typeof(UserProfile));
        }
    }
}
