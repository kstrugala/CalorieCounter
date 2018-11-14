using System;
using System.Threading.Tasks;
using CalorieCounter.Core.Domain;
using CalorieCounter.Infrastructure.EF;
using CalorieCounter.Infrastructure.Exceptions;
using CalorieCounter.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounter.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly CalorieCounterContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(CalorieCounterContext context, IPasswordHasher<User> passwordHasher)
        {
            _context=context;
            _passwordHasher = passwordHasher;
        }

        public async Task SignIn(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task SignUp(string email, string password, string firstName, string lastName)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.Email==email);

            if(user!=null)
                throw new ServiceException(ErrorCodes.EmailInUse, $"User with email:{email} already exists.");

            user = new User(Guid.NewGuid(), email, Role.User);
            user.SetPassword(password, _passwordHasher);
            user.SetFirstName(firstName);
            user.SetLastName(lastName);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

        }
    }
}