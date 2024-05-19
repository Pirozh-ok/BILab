using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.TypeOfDay;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers {
    [Authorize(Roles = Constants.NameRoleAdmin)]
    public class TypeOfDayController : BaseCrudController<ITypeOfDayService, TypeOfDayDTO, TypeOfDayDTO, Guid> {
        public TypeOfDayController(ITypeOfDayService service) : base(service) {
        }

        [AllowAnonymous]
        [HttpGet]
        public override async Task<IActionResult> GetAllAsync() {
            var result = await _service.GetAsync<TypeOfDayDTO>();
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<IActionResult> GetByIdAsync(Guid id) {
            var result = await _service.GetByIdAsync<TypeOfDayDTO>(id);
            return GetResult(result, (int)HttpStatusCode.OK);
        }
    }
}
