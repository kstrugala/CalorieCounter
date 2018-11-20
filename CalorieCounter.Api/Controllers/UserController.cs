using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalorieCounter.Infrastructure.Commands;
using CalorieCounter.Infrastructure.Commands.Users;
using CalorieCounter.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CalorieCounter.Api.Controllers
{
    [Authorize]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenManager _tokenManager;

        public UserController(ICommandDispatcher commandDispatcher, IUserService userService, ITokenManager tokenManager) : base(commandDispatcher)
        {
            _userService = userService;
            _tokenManager = tokenManager;
        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        {
            await CommandDispatcher.DispatchAsync(command);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SingInCommand command)
            => Ok(await _userService.SignIn(command.Email, command.Password));


        [AllowAnonymous]
        [HttpPost("tokens/{token}/refresh")]
        public async Task<IActionResult> RefreshAccessToken(string token)
            => Ok(await _userService.RefreshAccessToken(token));

        [HttpPost("tokens/{token}/revoke")]
        public async Task<IActionResult> RevokeRefreshToken(string token)
        {
            await _userService.RevokeRefreshToken(token);
            return NoContent();
        }
        
        [HttpPost("tokens/cancel")]
        public async Task<IActionResult> CancelAccessToken()
        {
            await _tokenManager.DeactivateCurrentToken();
            return NoContent();
        }
    }
}