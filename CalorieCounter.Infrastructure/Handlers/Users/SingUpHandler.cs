using System.Threading.Tasks;
using CalorieCounter.Infrastructure.Commands;
using CalorieCounter.Infrastructure.Commands.Users;
using CalorieCounter.Infrastructure.Services;

namespace CalorieCounter.Infrastructure.Handlers.Users
{
    public class SingUpHandler : ICommandHandler<SignUpCommand>
    {
        private readonly IUserService _userService;

        public SingUpHandler(IUserService userService)
        {
            _userService=userService;
        }

        public async Task HandleAsync(SignUpCommand command)
        {
            await _userService.SignUp(command.Email, command.Password, command.FirstName, command.LastName);
        }
    }
}