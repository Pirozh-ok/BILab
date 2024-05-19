using AutoMapper;
using AutoMapper.QueryableExtensions;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.User;
using BILab.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Web;

namespace BILab.BusinessLogic.Services.EntityServices {
    public class UserService : SearchableEntityService<UserService, User, Guid, UserDTO, PageableUserRequestDto>, IUserService {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IAccessService _accessService;
        private readonly RoleManager<Role> _roleManager;
        private readonly LinkGenerator _generator;

        public UserService(
            UserManager<User> userManager,
            ITokenService tokenService,
            IEmailService emailService,
            IAccessService accessService,
            IMapper mapper,
            ApplicationDbContext context,
            LinkGenerator generator,
            RoleManager<Role> roleManager) : base(context, mapper) {
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _accessService = accessService;
            _generator = generator;
            _roleManager = roleManager;
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
            user.Id = Guid.NewGuid();
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken.Token;
            user.ExpirationAt = refreshToken.ExpirationDate;

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, Constants.NameRoleUser);
                await SendConfirmationEmailAsync(user, ResponseConstants.TextConfirmEmail);

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
                await _userManager.AddToRoleAsync(user, Constants.NameRoleEmployee);
                await SendConfirmationEmailAsync(user, ResponseConstants.TextConfirmEmail);

                return ServiceResult.Ok(ResponseConstants.Created);
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        public async Task<ServiceResult> GetEmployeesAsync() {
            var employeeRole = _roleManager.FindByNameAsync(Constants.NameRoleEmployee).Result;

            var users = await _userManager.Users
            .AsNoTracking()
            .ToListAsync();

            var employees = new List<UserDTO>();

            foreach (var user in users) {
                if (_userManager.IsInRoleAsync(user, Constants.NameRoleEmployee).Result) {
                    employees.Add(_mapper.Map<UserDTO>(user));
                }
            }

            return ServiceResult.Ok(employees);
        }

        public async Task<ServiceResult> CreateAdmin(UserDTO dto) {
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
                await _userManager.AddToRoleAsync(user, Constants.NameRoleAdmin);
                await SendConfirmationEmailAsync(user, ResponseConstants.TextConfirmEmail);

                return ServiceResult.Ok(ResponseConstants.Created);
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        public override async Task<ServiceResult> UpdateAsync(UserDTO userDTO) {
            var validationResult = Validate(userDTO);

            if (validationResult.Failure) {
                return validationResult;
            }

            if (!_accessService.IsHasAccess(userDTO.Id)) {
                return ServiceResult.Fail(ResponseConstants.AccessDenied);
            }

            var user = await _userManager.FindByIdAsync(userDTO.Id.ToString());

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.UserNotFound);
            }

            var updateDto = _mapper.Map<UpdateUserDTO>(userDTO);
            user = _mapper.Map(updateDto, user);
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) {
                return ServiceResult.Ok();
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        public override async Task<ServiceResult> DeleteAsync(Guid userId) {
            if (!_accessService.IsHasAccess(userId)) {
                return ServiceResult.Fail(ResponseConstants.AccessDenied);
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.UserNotFound);
            }

            user.IsDeleted = true;
            user.DeletedAt = DateTime.Now;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? ServiceResult.Ok() :
                ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        public async Task<ServiceResult> ConfirmEmailAsync() {
            var userId = _accessService.GetUserIdFromRequest().ToString();
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.NotFound);
            }

            if (user.EmailConfirmed) {
                return ServiceResult.Ok(ResponseConstants.EmailAlreadyConfirmed);
            }

            await SendConfirmationEmailAsync(user, ResponseConstants.TextConfirmEmail);
            return ServiceResult.Ok(ResponseConstants.ChechkEmail);
        }

        public async Task<ServiceResult> VerificationConfirmationTokenAsync(string token, string email) {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.NotFound);
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded) {
                return ServiceResult.Ok(ResponseConstants.EmailConfirmed);
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        public async Task<ServiceResult> ChangePasswordAsync(ChangePasswordDTO changePasswordData) {
            var userId = _accessService.GetUserIdFromRequest().ToString();
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.NotFound);
            }

            if (!string.Equals(changePasswordData.NewPassword, changePasswordData.ConfirmNewPassword)) {
                return ServiceResult.Fail(ResponseConstants.PasswordsNotMatch);
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordData.OldPassword, changePasswordData.NewPassword);

            if (result.Succeeded) {
                await _emailService.SendEmailAsync(user.Email, ResponseConstants.SubjectPasswordChanged, ResponseConstants.EmailPasswordChanged);
                return ServiceResult.Ok(ResponseConstants.PasswordChanged);
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        public async Task<ServiceResult> ChangeEmailAsync(string newEmail) {
            var userId = _accessService.GetUserIdFromRequest().ToString();
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.NotFound);
            }

            var validateEmail = ValidateEmail(newEmail);

            if (!validateEmail.isValid) {
                return ServiceResult.Fail(validateEmail.message);
            }

            user.Email = newEmail;
            user.EmailConfirmed = false;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) {
                await SendConfirmationEmailAsync(user, ResponseConstants.ConfirmUpdatedEmail);
                return ServiceResult.Ok(ResponseConstants.ChechkEmail);
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        public async Task<ServiceResult> SendResetPasswordEmailAsync(string email) {
            var validateEmail = ValidateEmail(email);

            if (!validateEmail.isValid) {
                return ServiceResult.Fail(validateEmail.message);
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.NotFound);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = $"https://frontend/reset-password?token={token}";

            await _emailService.SendEmailAsync(user.Email, ResponseConstants.SubjectResetPassword,
                string.Format(ResponseConstants.TextResetEmail, user.UserName, $"<a href=\"{callbackUrl}\">link</a>"));

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> ResetPasswordAsync(ResetPasswordDTO resetPasswordData) {
            var resultValid = ValidateResetPasswordData(resetPasswordData);

            if (resultValid.Failure) {
                return resultValid;
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordData.Email);

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.NotFound);
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordData.Token, resetPasswordData.NewPassword);

            if (result.Succeeded) {
                return ServiceResult.Ok();
            }

            return ServiceResult.Fail(result.Errors
                .Select(x => x.Description)
                .ToList());
        }

        public override async Task<ServiceResult> GetAsync<TGetUserDto>() {
            return ServiceResult.Ok(await _userManager.Users
                .ProjectTo<TGetUserDto>(_mapper.ConfigurationProvider)
                .ToListAsync());
        }

        public override async Task<ServiceResult> GetByIdAsync<TGetUserDto>(Guid id) {
            var user = await _userManager.FindByIdAsync(id.ToString());

            return user is null ? ServiceResult.Fail(ResponseConstants.NotFound)
                : ServiceResult.Ok(_mapper.Map<TGetUserDto>(user));
        }

        private async Task SendConfirmationEmailAsync(User user, string message) {
            if (user is null) {
                return;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = HttpUtility.UrlEncode(token);

            //var callbackUrl = _generator.GetUriByAction(_accessor.HttpContext,
            //    action: "Confirm-Email",
            //    controller: "Account",
            //    values: new { token = token, email = user.Email });
            var scheme = _accessService.GetSchemeFromRequest();
            var host = _accessService.GetHostFromRequest();
            var callbackUrl = $"{scheme}://{host}/api/account/confirm-email?token={token}&email={user.Email}";

            await _emailService.SendEmailAsync(user.Email, ResponseConstants.SubjectConfirmEmail,
                string.Format(message, $"{user.LastName} {user.FirstName} {user.Patronymic ?? string.Empty}", $"<a href=\"{callbackUrl}\">link</a>"));
        }

        private ServiceResult ValidateResetPasswordData(ResetPasswordDTO resetPasswordData) {
            if (resetPasswordData is null) {
                return ServiceResult.Fail(ResponseConstants.NullArgument);
            }

            var validateNewPassword = ValidatePassword(resetPasswordData.NewPassword);

            if (!validateNewPassword.isValid) {
                return ServiceResult.Fail(validateNewPassword.message);
            }

            var validateConfirmPassword = ValidatePassword(resetPasswordData.ConfirmPassword);

            if (!validateConfirmPassword.isValid) {
                return ServiceResult.Fail(validateConfirmPassword.message);
            }

            if (!string.Equals(resetPasswordData.NewPassword, resetPasswordData.ConfirmPassword)) {
                return ServiceResult.Fail(ResponseConstants.PasswordsNotMatch);
            }

            var validateEmail = ValidateEmail(resetPasswordData.Email);

            if (!validateEmail.isValid) {
                return ServiceResult.Fail(validateEmail.message);
            }

            return ServiceResult.Ok();
        }

        private (bool isValid, string message) ValidateEmail(string email) {
            if (string.IsNullOrEmpty(email) || email.Length < Constants.MinLenOfEmail) {
                return (false, ResponseConstants.EmailLessMinLen);
            }

            if (email.Length > Constants.MaxLenOfEmail) {
                return (false, ResponseConstants.EmailExceedsMaxLen);
            }

            return (true, string.Empty);
        }

        private (bool isValid, string message) ValidatePassword(string password) {
            if (string.IsNullOrEmpty(password) || password.Length < Constants.MinLenOfPassword) {
                return (false, ResponseConstants.PasswordLessMinLen);
            }

            if (password.Length > Constants.MaxLenOfPassword) {
                return (false, ResponseConstants.PasswordExceedsMaxLen);
            }

            return (true, string.Empty);
        }

        protected override ServiceResult Validate(UserDTO user) {
            var errors = new List<string>();

            if (user is null) {
                errors.Add(ResponseConstants.NullArgument);
                return BuildValidateResult(errors);
            }

            if (string.IsNullOrEmpty(user.FirstName) || user.FirstName.Length < Constants.MinLenOfName) {
                errors.Add(ResponseConstants.FirstNameLessMinLen);
            }

            if((int)user.Sex < 0 || (int)user.Sex > 2) {
                errors.Add(ResponseConstants.IncorrectedSex);
            }

            if (user.FirstName.Length > Constants.MaxLenOfName) {
                errors.Add(ResponseConstants.FirstNameExceedsMaxLen);
            }

            if (string.IsNullOrEmpty(user.LastName) || user.LastName.Length < Constants.MinLenOfName) {
                errors.Add(ResponseConstants.LastNameLessMinLen);
            }

            if (user.LastName.Length > Constants.MaxLenOfName) {
                errors.Add(ResponseConstants.LastNameExceedsMaxLen);
            }

            if (user.Patronymic is not null && user.Patronymic.Length < Constants.MinLenOfName) {
                errors.Add(ResponseConstants.PatronymicLessMinLen);
            }
            else if (user.Patronymic is null) {
                user.Patronymic = string.Empty;
            }

            if (user.Patronymic is not null && user.Patronymic.Length > Constants.MaxLenOfName) {
                errors.Add(ResponseConstants.PatronymicExceedsMaxLen);
            }

            if (user.DateOfBirth > DateTime.UtcNow.AddYears(-18) || user.DateOfBirth > DateTime.UtcNow) {
                errors.Add(ResponseConstants.IncorrectDateOfBirth);
            }

            if (user.PhoneNumber is null) {
                errors.Add(ResponseConstants.IncorrectPhoneNumber);
            }

            if (user.IsNew) {
                var emailValidate = ValidateEmail(user.Email);
                var passwordValidate = ValidatePassword(user.Password);

                if (!emailValidate.isValid) {
                    errors.Add(emailValidate.message);
                }

                if (!passwordValidate.isValid) {
                    errors.Add(passwordValidate.message);
                }
            }

            return BuildValidateResult(errors);
        }

        protected override List<Expression<Func<User, bool>>> GetAdvancedConditions(PageableUserRequestDto filters) {
            var conditions = new List<Expression<Func<User, bool>>>();

            if (filters.Sex.HasValue) {
                conditions.Add(x => x.Sex == filters.Sex);
            }

            if (filters.AgeFrom is not null) {
                conditions.Add(x => DateTime.UtcNow.Year - x.DateOfBirth.Year > filters.AgeFrom);
            }

            if (filters.AgeTo is not null) {
                conditions.Add(x => DateTime.UtcNow.Year - x.DateOfBirth.Year < filters.AgeTo);
            }

            return conditions;
        }

        protected override IQueryable<User> GetEntityByIdIncludes(IQueryable<User> query) {
            return query;
        }
    }
}