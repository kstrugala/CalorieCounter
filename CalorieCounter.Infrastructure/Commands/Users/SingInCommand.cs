namespace CalorieCounter.Infrastructure.Commands.Users
{
    public class SingInCommand : ICommand
    {
        public string Email { get; set; } 
        public string Password { get; set; }
    }
}