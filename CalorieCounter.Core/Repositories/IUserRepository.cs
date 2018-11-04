using System;
using System.Threading.Tasks;
using CalorieCounter.Core.Domain;

namespace CalorieCounter.Core.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}