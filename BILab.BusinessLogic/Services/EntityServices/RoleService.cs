using AutoMapper;
using AutoMapper.QueryableExtensions;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Role;
using BILab.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BILab.BusinessLogic.Services.EntityServices {
    public class RoleServices : BaseEntityService<Role, Guid, RoleDTO>, IRoleService {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleServices(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ApplicationDbContext context,
            IMapper mapper) : base(context, mapper) {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ServiceResult> AddRolesToUserAsync(Guid userId, string[] roleNames) {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.UserNotFound);
            }

            var validationResult = RolesIsExist(roleNames);

            if (validationResult.Failure) {
                return validationResult;
            }

            var result = await _userManager.AddToRolesAsync(user, roleNames);

            if (result.Succeeded) {
                return ServiceResult.Ok(ResponseConstants.RoleAddedToUser);
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        public override async Task<ServiceResult> CreateAsync(RoleDTO dto) {
            var validationResult = Validate(dto);

            if (validationResult.Failure) {
                return validationResult;
            }

            var result = await _roleManager.CreateAsync(
                new Role {
                    Name = dto.Name
                });

            if (result.Succeeded) {
                return ServiceResult.Ok(ResponseConstants.RoleCreated);
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        public override async Task<ServiceResult> DeleteAsync(Guid id) {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return await DeleteRole(role);
        }

        public async Task<ServiceResult> DeleteAsync(string name) {
            var role = await _roleManager.FindByNameAsync(name);
            return await DeleteRole(role);
        }

        public override async Task<ServiceResult> GetAsync<GetRoleDto>() =>
            ServiceResult.Ok(await _roleManager.Roles.
                ProjectTo<GetRoleDto>(_mapper.ConfigurationProvider)
                .ToListAsync());

        public async Task<ServiceResult> GetUserRolesAsync(Guid userId) {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.RoleNotFound);
            }

            return ServiceResult.Ok(await _userManager.GetRolesAsync(user));
        }

        public async Task<ServiceResult> RemoveRolesFromUserAsync(Guid userId, string[] roleNames) {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.RoleNotFound);
            }

            var validationResult = RolesIsExist(roleNames);

            if (validationResult.Failure) {
                return validationResult;
            }

            var result = await _userManager.RemoveFromRolesAsync(user, roleNames);

            if (result.Succeeded) {
                return ServiceResult.Ok(ResponseConstants.RoleAddedToUser);
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        public override async Task<ServiceResult> UpdateAsync(RoleDTO dto) {
            var validationResult = Validate(dto);

            if (validationResult.Failure) {
                return validationResult;
            }

            var role = await _roleManager.FindByIdAsync(dto.Id.ToString());

            if (role is null) {
                return ServiceResult.Fail(ResponseConstants.RoleNotFound);
            }

            role.Name = dto.Name;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded) {
                return ServiceResult.Ok(ResponseConstants.RoleUpdated);
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        private async Task<ServiceResult> DeleteRole(Role role) {
            if (role is null) {
                return ServiceResult.Fail(ResponseConstants.RoleNotFound);
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded) {
                return ServiceResult.Ok(ResponseConstants.RoleDeleted);
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        protected override ServiceResult Validate(RoleDTO dto) {
            var errors = new List<string>();

            if (dto is null) {
                return ServiceResult.Fail(ResponseConstants.NullArgument);
            }

            if (string.IsNullOrEmpty(dto.Name)) {
                return ServiceResult.Fail(ResponseConstants.RoleNullOrEmptyName);
            }

            if (dto.Name.Length > Constants.MaxLenOfName) {
                errors.Add(string.Format(ResponseConstants.RoleNameExceedMaxLen, Constants.MaxLenOfName));
            }

            if (dto.Name.Length < Constants.MinLenOfName) {
                errors.Add(string.Format(ResponseConstants.RoleNameLessMinLen, Constants.MinLenOfName));
            }

            return errors.Count > 0 ? ServiceResult.Fail(errors) : ServiceResult.Ok();
        }

        private ServiceResult RolesIsExist(string[] roleNames) {
            var errors = new List<string>();
            var existingRoles = _roleManager.Roles
                .Select(x => x.Name)
                .ToHashSet();

            foreach (var role in roleNames) {
                if (existingRoles.Contains(role.ToLower())) {
                    errors.Add(string.Format(ResponseConstants.RoleNotExist, role));
                }
            }

            return errors.Count > 0 ? ServiceResult.Fail(errors) : ServiceResult.Ok();
        }
    }
}
