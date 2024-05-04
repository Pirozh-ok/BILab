using BILab.Domain.DTOs.Tokens;
using BILab.Domain.Models.Entities;

namespace BILab.Domain.Contracts.Services {
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(User user);
        public RefreshTokenDTO GenerateRefreshToken();
        Task<ServiceResult> GetNewTokens(string jwtToken, string refresh);
    }
}
