using System.Threading.Tasks;

namespace CalorieCounter.Infrastructure.Services
{
    public interface ITokenManager
    {
         Task<bool> IsCurrentActiveToken();
         Task DeactivateCurrentToken();
         Task<bool> IsActiveAsync(string token);
         Task DeactivateAsync(string token);
    }
}