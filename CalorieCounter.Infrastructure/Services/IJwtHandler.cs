using CalorieCounter.Core.Domain;

namespace CalorieCounter.Infrastructure.Services
{
    public interface IJwtHandler
    {
         JsonWebToken Create(string email, string role);
    }
}