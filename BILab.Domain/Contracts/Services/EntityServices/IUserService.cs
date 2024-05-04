using BILab.Domain.Contracts.Services.Base;
using BILab.Domain.DTOs.Base;
using BILab.Domain.DTOs.User;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Contracts.Services.EntityServices
{
    public interface IUserService : ISearchableEntityService<User, Guid, UserDTO, PageableBaseRequestDto>
    {
        Task<ServiceResult> LoginAsync(LoginDTO dto);
        Task<ServiceResult> ConfirmEmailAsync();
        Task<ServiceResult> VerificationConfirmationTokenAsync(string token, string email);
        Task<ServiceResult> SendResetPasswordEmailAsync(string email);
        Task<ServiceResult> ResetPasswordAsync(ResetPasswordDTO resetPasswordData);
        Task<ServiceResult> ChangePasswordAsync(ChangePasswordDTO changePasswordData);
        Task<ServiceResult> ChangeEmailAsync(string newEmail);
    }
}
