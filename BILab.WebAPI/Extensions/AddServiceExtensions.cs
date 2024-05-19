using BILab.BusinessLogic.Services.EntityServices;
using BILab.BusinessLogic.Services;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.Contracts.Services;

namespace BILab.Web.Extensions {
    public static class AddServicesExtensions {
        public static void AddUserServices(this IServiceCollection services) {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAccessService, AccessService>();
            services.AddTransient<ITypeOfDayService, TypeOfDayService>();
            services.AddTransient<ISheduleService, SheduleService>();
            services.AddTransient<IAdressService, AdressService>();
            services.AddTransient<IRoleService, RoleServices>();
            services.AddTransient<ISpecialOfferService, SpecialOfferService>();
            services.AddTransient<IProcedureService, ProcedureService>();
            services.AddTransient<IRecordService, RecordService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
