using BILab.BusinessLogic.Services.EntityServices;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.SpecialOffer;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;

namespace BILab.WebAPI.Controllers {
    [Authorize(Roles = Constants.NameRoleAdmin)]
    public class SpecialOfferController : BaseCrudController<ISpecialOfferService, SpecialOfferDTO, SpecialOfferDTO, Guid> {
        public SpecialOfferController(SpecialOfferService service) : base(service) {
        }
    }
}
