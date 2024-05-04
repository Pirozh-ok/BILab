using AutoMapper;
using BILab.BusinessLogic.Services.Base;
using BILab.DataAccess;
using BILab.Domain;
using BILab.Domain.Contracts.Services;
using BILab.Domain.DTOs.Tokens;
using BILab.Domain.Models.Entities;
using BILab.Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BILab.BusinessLogic.Services {
    public class TokenService : BaseService, ITokenService {
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly UserManager<User> _userManager;

        public TokenService(
            IMapper mapper,
            ApplicationDbContext context,
            IOptions<JwtSettings> jwtSettings,
            UserManager<User> userManager) : base(mapper, context) {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<string> GenerateAccessToken(User user) {
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));
            var claims = await GetClaimsByUser(user);

            var tokenDescription = new SecurityTokenDescriptor {
                Issuer = _jwtSettings.Value.Issuer,
                Audience = _jwtSettings.Value.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.Value.TokenValidityInSecond),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public async Task<ServiceResult> GetNewTokens(string jwtToken, string refresh) {
            if (string.IsNullOrEmpty(jwtToken) || string.IsNullOrEmpty(refresh)) {
                return ServiceResult.Fail(ResponseConstants.InvalidAccessOrRefreshToken);
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.RefreshToken.ToString() == refresh);

            if (user is null) {
                return ServiceResult.Fail(ResponseConstants.InvalidAccessOrRefreshToken);
            }

            if (user.ExpirationAt < DateTime.Now) {
                return ServiceResult.Fail(ResponseConstants.InvalidAccessOrRefreshToken);
            }

            var newRefreshToken = GenerateRefreshToken();
            var newAccessToken = await GenerateAccessToken(user);

            user.RefreshToken = newRefreshToken.Token;
            user.ExpirationAt = newRefreshToken.ExpirationDate;
            await _userManager.UpdateAsync(user);

            return ServiceResult.Ok(
                new TokensDTO {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken.Token.ToString()
                });
        }

        public RefreshTokenDTO GenerateRefreshToken() {
            return new RefreshTokenDTO() {
                Token = Guid.NewGuid(),
                ExpirationDate = DateTime.UtcNow.AddDays(_jwtSettings.Value.RefreshTokenValidityInDays)
            };
        }

        private async Task<List<Claim>> GetClaimsByUser(User user) {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
            };

            foreach (var userRole in userRoles) {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return authClaims;
        }
    }
}