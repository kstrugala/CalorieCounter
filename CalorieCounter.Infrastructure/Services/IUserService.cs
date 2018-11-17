using System.Threading.Tasks;
using CalorieCounter.Core.Domain;

namespace CalorieCounter.Infrastructure.Services
{
    public interface IUserService : IService
    {
         Task SignUp(string email, string password, string firsName, string lastName);
         Task<JsonWebToken> SignIn(string email, string password);
         Task<JsonWebToken> RefreshAccessToken(string token);
         Task RevokeRefreshToken(string token);
    }
}