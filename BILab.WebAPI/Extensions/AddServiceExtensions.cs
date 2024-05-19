using BILab.BusinessLogic.Services.EntityServices;
using BILab.BusinessLogic.Services;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.Contracts.Services;

namespace BILab.Web.Extensions {
    public static class AddServicesExtensions {
        public static void AddUserServices(this IServiceCollection services) {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAccessService, AccessService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ITypeOfDayService, TypeOfDayService>();
            services.AddTransient<IAdressService, AdressService>();
            services.AddTransient<IRoleService, RoleServices>();
            services.AddTransient<IProcedureService, ProcedureService>();
            services.AddTransient<ISheduleService, SheduleService>();
            services.AddTransient<ISpecialOfferService, SpecialOfferService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRecordService, RecordService>();
        }
    }
}