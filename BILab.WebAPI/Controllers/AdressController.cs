using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Adress;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;

namespace BILab.WebAPI.Controllers {
    [Authorize(Roles = Constants.NameRoleAdmin)]
    public class AdressController : BaseCrudController<IAdressService, AdressDTO, AdressDTO, Guid> {
        public AdressController(IAdressService service) : base(service) {
        }
    }
}
