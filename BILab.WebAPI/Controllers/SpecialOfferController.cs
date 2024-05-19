using BILab.BusinessLogic.Services.EntityServices;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.SpecialOffer;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers {
    [Authorize(Roles = Constants.NameRoleAdmin)]
    public class SpecialOfferController : BaseCrudController<ISpecialOfferService, SpecialOfferDTO, SpecialOfferDTO, Guid> {
        public SpecialOfferController(ISpecialOfferService service) : base(service) {
        }

        [AllowAnonymous]
        [HttpGet]
        public override async Task<IActionResult> GetAllAsync() {
            var result = await _service.GetAsync<SpecialOfferDTO>();
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<IActionResult> GetByIdAsync(Guid id) {
            var result = await _service.GetByIdAsync<SpecialOfferDTO>(id);
            return GetResult(result, (int)HttpStatusCode.OK);
        }
    }
}
