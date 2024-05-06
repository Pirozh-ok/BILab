using BILab.BusinessLogic.Services.EntityServices;
using BILab.Domain.DTOs.SpecialOffer;
using BILab.WebAPI.Controllers.Base;

namespace BILab.WebAPI.Controllers {
    public class SpecialOfferController : BaseCrudController<SpecialOfferService, SpecialOfferDTO, SpecialOfferDTO, Guid> {
        public SpecialOfferController(SpecialOfferService service) : base(service) {
        }
    }
}
