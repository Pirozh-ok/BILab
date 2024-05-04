using AutoMapper;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Base;
using BILab.Domain.DTOs.User;
using BILab.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace BILab.BusinessLogic.Services.EntityServices
{
    public class UserService : SearchableEntityService<UserService, User, Guid, UserDTO, PageableBaseRequestDto>, IUserService {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IAccessService _accessService;
        private readonly LinkGenerator _generator;

        public UserService(
            UserManager<User> userManager,
            ITokenService tokenService,
            IEmailService emailService,
            IAccessService accessService,
            IMapper mapper,
            ApplicationDbContext context,
            LinkGenerator generator) : base(context, mapper) {
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _accessService = accessService;
            _generator = generator;
        }

        public async Task<ServiceResult> LoginAsync(LoginDTO dto) {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.InvalidEmailOrPassword);
            }

            if (await _userManager.CheckPasswordAsync(user, dto.Password)) {
                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken.Token;
                user.ExpirationAt = refreshToken.ExpirationDate;
                await _userManager.UpdateAsync(user);

                return ServiceResult.Ok(
                    new AuthResponseDTO<GetUserDTO> {
                        Data = _mapper.Map<GetUserDTO>(user),
                        AccessToken = await _tokenService.GenerateAccessToken(user),
                        RefreshToken = refreshToken.Token.ToString()
                    });
            }

            return ServiceResult.Fail(ResponseConstants.InvalidEmailOrPassword);
        }

        public override async Task<ServiceResult> CreateAsync(UserDTO dto) {
            var validationResult = Validate(dto);

            if (validationResult.Failure) {
                return validationResult;
            }

            var user = _mapper.Map<User>(dto);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken.Token;
            user.ExpirationAt = refreshToken.ExpirationDate;

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, Constants.NameRoleUser);
                //await SendConfirmationEmailAsync(user, UserResource.TextConfirmEmail);

                return ServiceResult.Ok(
                    new AuthResponseDTO<GetUserDTO> {
                        Data = _mapper.Map<GetUserDTO>(user),
                        AccessToken = await _tokenService.GenerateAccessToken(user),
                        RefreshToken = refreshToken.Token.ToString()
                    });
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        public async Task<ServiceResult> CreateEmployee(UserDTO dto) {
            var validationResult = Validate(dto);

            if (validationResult.Failure) {
                return validationResult;
            }

            var user = _mapper.Map<User>(dto);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken.Token;
            user.ExpirationAt = refreshToken.ExpirationDate;

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, Constants.NameRoleUser);
                //await SendConfirmationEmailAsync(user, UserResource.TextConfirmEmail);

                return ServiceResult.Ok(
                    new AuthResponseDTO<GetUserDTO> {
                        Data = _mapper.Map<GetUserDTO>(user),
                        AccessToken = await _tokenService.GenerateAccessToken(user),
                        RefreshToken = refreshToken.Token.ToString()
                    });
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }


        public Task<ServiceResult> ChangeEmailAsync(string newEmail) {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> ChangePasswordAsync(ChangePasswordDTO changePasswordData) {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> ConfirmEmailAsync() {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> ResetPasswordAsync(ResetPasswordDTO resetPasswordData) {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> SendResetPasswordEmailAsync(string email) {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> VerificationConfirmationTokenAsync(string token, string email) {
            throw new NotImplementedException();
        }

        protected override ServiceResult Validate(UserDTO dto) {
            throw new NotImplementedException();
        }
    }
}
