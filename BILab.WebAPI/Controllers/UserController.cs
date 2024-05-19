using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.User;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers {
    public class UserController : BaseCrudController<IUserService, UserDTO, GetUserDTO, Guid> {
        public UserController(IUserService service) : base(service) {
        }

        [HttpPost]
        [AllowAnonymous]
        public override async Task<IActionResult> CreateAsync(UserDTO dto) {
            var result = await _service.CreateAsync(dto);
            return GetResult(result, (int)HttpStatusCode.Created);
        }

        [HttpPost("/add-admin")]
        [Authorize(Roles = Constants.NameRoleAdmin)]
        public async Task<IActionResult> CreateAdminAsync(UserDTO dto) {
            var result = await _service.CreateAdmin(dto);
            return GetResult(result, (int)HttpStatusCode.Created);
        }

        [HttpPost("/add-employee")]
        [Authorize(Roles = Constants.NameRoleAdmin)]
        public async Task<IActionResult> CreateEmployeeAsync(UserDTO dto) {
            var result = await _service.CreateEmployee(dto);
            return GetResult(result, (int)HttpStatusCode.Created);
        }

        [HttpPost("/employee")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEmployees() {
            var result = await _service.GetEmployeesAsync();
            return GetResult(result, (int)HttpStatusCode.Created);
        }

        [HttpGet]
        [Authorize(Roles = Constants.NameRoleAdmin)]
        public override async Task<IActionResult> GetAllAsync() {
            var result = await _service.GetAsync<GetUserDTO>();
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = Constants.NameRoleAdmin)]
        public override async Task<IActionResult> GetByIdAsync(Guid userId) {
            var result = await _service.GetByIdAsync<GetUserDTO>(userId);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [Authorize]
        [HttpGet("search")]
        public IActionResult GetFilteringUsers([FromQuery] PageableUserRequestDto filters) {
            var result = _service.SearchFor<GetUserDTO>(filters);
            return GetResult(result, (int)HttpStatusCode.OK);
        }
    }
}