using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Adress;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers {
    [Authorize(Roles = Constants.NameRoleAdmin)]
    public class AdressController : BaseCrudController<IAdressService, AdressDTO, AdressDTO, Guid> {
        public AdressController(IAdressService service) : base(service) {
        }

        [AllowAnonymous]
        [HttpGet]
        public override async Task<IActionResult> GetAllAsync() {
            var result = await _service.GetAsync<AdressDTO>();
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<IActionResult> GetByIdAsync(Guid id) {
            var result = await _service.GetByIdAsync<AdressDTO>(id);
            return GetResult(result, (int)HttpStatusCode.OK);
        }
    }
}
