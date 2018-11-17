using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IJwtHandler _jwtHandler;

        public UserService(CalorieCounterContext context, IPasswordHasher<User> passwordHasher, IJwtHandler jwtHandler)
        {
            _context=context;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
        }

        public async Task<JsonWebToken> SignIn(string email, string password)
        {
            var user = await GetUser(email);
            
            if(user==null)
                throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");

            if(!user.ValidatePassword(password, _passwordHasher))
                throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");
            
            var jwt = _jwtHandler.Create(user.Email, user.Role);

            var refreshTokenFromDb = await GetRefreshTokenByEmail(user.Email);

            if(refreshTokenFromDb==null)
            {
                var refreshToken = _passwordHasher.HashPassword(user, Guid.NewGuid().ToString())
                    .Replace("+", string.Empty)
                    .Replace("=", string.Empty)
                    .Replace("/", string.Empty);
                jwt.RefreshToken = refreshToken;

                await AddRefreshToken(new RefreshToken{Email = email, Token=refreshToken});
            }
            else
            {
                jwt.RefreshToken = refreshTokenFromDb.Token;
            }
            
            return jwt;

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

         public async Task<JsonWebToken> RefreshAccessToken(string token)
        {
            var refreshToken = await GetRefreshToken(token);
            
            if (refreshToken == null)
            {
                throw new Exception("Refresh token was not found or was revoked.");
            }

            var user = await GetUser(refreshToken.Email);

            var jwt = _jwtHandler.Create(refreshToken.Email, user.Role);
            jwt.RefreshToken = refreshToken.Token;

            return jwt;
        }

        public async Task RevokeRefreshToken(string token)
        {
            var refreshToken = await GetRefreshToken(token);
            if (refreshToken == null)
            {
                throw new Exception("Refresh token was not found or was revoked.");
            }
            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();
        }

        private async Task<User> GetUser(string email)
            => await _context.Users.SingleOrDefaultAsync(x=>x.Email==email);

        private async Task<RefreshToken> GetRefreshToken(string token)
            => await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == token);

        private async Task<RefreshToken> GetRefreshTokenByEmail(string email)
            => await _context.RefreshTokens.SingleOrDefaultAsync(x=>x.Email==email);

        private async Task AddRefreshToken(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }
    }
}