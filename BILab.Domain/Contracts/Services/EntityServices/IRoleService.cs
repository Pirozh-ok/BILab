using BILab.Domain.Contracts.Services.Base;
using BILab.Domain.DTOs.Role;

namespace BILab.Domain.Contracts.Services.EntityServices {
    public interface IRoleService : IBaseEntityService<Guid, RoleDTO> {
        Task<ServiceResult> AddRolesToUserAsync(Guid userId, string[] roleNames);
        Task<ServiceResult> RemoveRolesFromUserAsync(Guid userId, string[] roleNames);
        Task<ServiceResult> GetUserRolesAsync(Guid userId);
    }
}
