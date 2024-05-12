using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Shedule;
using BILab.Domain.DTOs.User;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers {
    [Authorize(Roles = $"{Constants.NameRoleAdmin}, {Constants.NameRoleEmployee}")]
    public class SheduleController : BaseCrudController<ISheduleService, SheduleDTO, SheduleDTO, Guid> {
        public SheduleController(ISheduleService service) : base(service) {
        }

        [Authorize]
        [HttpGet("search")]
        public IActionResult GetFilteringShedules([FromQuery] PageableSheduleRequestDto filters) {
            var result = _service.SearchFor<SheduleDTO>(filters);
            return GetResult(result, (int)HttpStatusCode.OK);
        }
    }
}
