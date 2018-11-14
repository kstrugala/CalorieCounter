using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalorieCounter.Infrastructure.Commands;
using CalorieCounter.Infrastructure.Commands.Users;
using Microsoft.AspNetCore.Mvc;
namespace CalorieCounter.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        public UserController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        {
            await CommandDispatcher.DispatchAsync(command);
            return Ok();
        }

    }
}