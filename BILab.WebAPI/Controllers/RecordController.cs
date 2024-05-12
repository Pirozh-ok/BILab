using BILab.BusinessLogic.Services.EntityServices;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Record;
using BILab.Domain.DTOs.User;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers {
    [Authorize]
    public class RecordController : BaseCrudController<IRecordService, RecordDTO, GetShortedRecordDTO, Guid> {
        public RecordController(RecordService service) : base(service) {
        }


        [Authorize(Roles = Constants.NameRoleUser)]
        [HttpPost]
        public override async Task<IActionResult> CreateAsync([FromBody] RecordDTO createDto) {
            var result = await _service.CreateAsync(createDto);
            return GetResult(result, (int)HttpStatusCode.Created);
        }

        [Authorize]
        [HttpGet("search")]
        public IActionResult GetFilteringRecords([FromQuery] PageableRecordRequestDto filters) {
            var result = _service.SearchFor<GetFullRecordDTO>(filters);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [Authorize]
        [HttpGet("user-records/{userId}")]
        public async Task<IActionResult> GetUserRecords(Guid userId) {
            var result = await _service.GetRecordsByUserId(userId);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpGet("employee-records/{userId}")]
        public async Task<IActionResult> GetEmployeeRecords(Guid userId) {
            var result = await _service.GetRecordsByEmployeeId(userId);
            return GetResult(result, (int)HttpStatusCode.OK);
        }
    }
}
