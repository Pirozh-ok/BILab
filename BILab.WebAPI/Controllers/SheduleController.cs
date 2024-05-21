using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Shedule;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers {
    [Authorize(Roles = $"{Constants.NameRoleAdmin}, {Constants.NameRoleEmployee}")]
    public class SheduleController : BaseCrudController<ISheduleService, SheduleDTO, SheduleDTO, Guid> {
        public SheduleController(ISheduleService service) : base(service) {
        }

        [AllowAnonymous]
        [HttpGet]
        public override async Task<IActionResult> GetAllAsync() {
            var result = await _service.GetAsync<SheduleDTO>();
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<IActionResult> GetByIdAsync(Guid id) {
            var result = await _service.GetByIdAsync<SheduleDTO>(id);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public IActionResult GetFilteringShedules([FromQuery] PageableSheduleRequestDto filters) {
            var result = _service.SearchFor<SheduleDTO>(filters);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [Authorize]
        [HttpGet("/{employeeId}")]
        public async Task<IActionResult> GetFreeSchedule(Guid employeeId, [FromQuery] DateTime dayOfWeek) {
            var result = await _service.GetFreeShedule(employeeId, dayOfWeek);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [Authorize]
        [HttpGet("/{employeeId}/schedules")]
        public async Task<IActionResult> GetSchedulesByEmployee(Guid employeeId) {
            var result = await _service.GetScheduleByEmployee(employeeId);
            return GetResult(result, (int)HttpStatusCode.OK);
        }
    }
}