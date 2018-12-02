using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CalorieCounter.Api.Migrations;
using CalorieCounter.Infrastructure.Commands;
using CalorieCounter.Infrastructure.Commands.FoodLog;
using CalorieCounter.Infrastructure.Commands.Users;
using CalorieCounter.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CalorieCounter.Core.Domain;
namespace CalorieCounter.Api.Controllers
{
    [Authorize]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFoodLogService _foodLogService;
        private readonly ITokenManager _tokenManager;

        public UserController(ICommandDispatcher commandDispatcher, IUserService userService, IFoodLogService foodLogService, ITokenManager tokenManager) : base(commandDispatcher)
        {
            _userService = userService;
            _foodLogService = foodLogService;
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
        [HttpPost("tokens/refresh")]
        public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshTokenModel refreshToken)
            => Ok(await _userService.RefreshAccessToken(refreshToken.Token));

        [HttpPost("tokens/revoke")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenModel refreshToken)
        {
            await _userService.RevokeRefreshToken(refreshToken.Token);
            return NoContent();
        }
        
        [HttpPost("tokens/cancel")]
        public async Task<IActionResult> CancelAccessToken()
        {
            await _tokenManager.DeactivateCurrentToken();
            return NoContent();
        }

        [HttpGet("foodlog")]
        public async Task<IActionResult> GetFoodLog()
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = await _userService.GetUserIdAsync(email);
            var foodLog = await _foodLogService.GetFoodLogAsync(userId);
            return Json(foodLog);
        }

        [HttpPost("foodlog")]
        public async Task<IActionResult> AddToFoodLog([FromBody] AddToFoodLogCommand command)
        {
            var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = await _userService.GetUserIdAsync(email);
            command.UserId=userId;

            await CommandDispatcher.DispatchAsync<AddToFoodLogCommand>(command);

            return NoContent();
        }

    }
}