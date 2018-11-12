using CalorieCounter.Infrastructure.EF;
using CalorieCounter.Infrastructure.Services;

namespace CalorieCounter.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly CalorieCounterContext _context;

        public UserService(CalorieCounterContext context)
        {
            _context=context;
        }
    }
}