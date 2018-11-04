using System;
using System.Threading.Tasks;
using CalorieCounter.Core.Domain;
using CalorieCounter.Core.Repositories;

namespace CalorieCounter.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}