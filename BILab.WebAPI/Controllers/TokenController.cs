using BILab.Domain.Contracts.Services;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers {
    public class TokenController : BaseController {
        private ITokenService _tokenService;

        public TokenController(ITokenService tokenService) {
            _tokenService = tokenService;
        }

        [HttpGet("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewTokens([FromQuery] string jwtToken, [FromQuery] string resreshToken) {
            var result = await _tokenService.GetNewTokens(jwtToken, resreshToken);
            return GetResult(result, (int)HttpStatusCode.OK);
        }
    }
}
