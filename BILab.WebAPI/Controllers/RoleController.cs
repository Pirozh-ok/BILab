using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Role;
using BILab.WebAPI.Controllers.Base;
using KinoPoisk.DomainLayer.DTOs.RoleDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers {
    [Authorize(Roles = Constants.NameRoleAdmin)]
    public class RoleController : BaseCrudController<IRoleService, RoleDTO, GetRoleDto, Guid> {

        public RoleController(IRoleService roleService) : base(roleService) {
        }
        [HttpGet("user-roles/{id}")]
        public async Task<IActionResult> GetUserRoles(Guid id) {
            var result = await _service.GetUserRolesAsync(id);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [HttpPut("add-roles")]
        public async Task<IActionResult> AddRolesToUser([FromQuery] Guid userId, [FromQuery] string[] roles) {
            var result = await _service.AddRolesToUserAsync(userId, roles);
            return GetResult(result, (int)HttpStatusCode.NoContent);
        }

        [HttpPut("remove-roles")]
        public async Task<IActionResult> RemoveRolesFromUser([FromQuery] Guid userId, [FromQuery] string[] roles) {
            var result = await _service.RemoveRolesFromUserAsync(userId, roles);
            return GetResult(result, (int)HttpStatusCode.NoContent);
        }
    }
}
