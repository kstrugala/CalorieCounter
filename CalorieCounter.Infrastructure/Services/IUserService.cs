using System.Threading.Tasks;

namespace CalorieCounter.Infrastructure.Services
{
    public interface IUserService : IService
    {
         Task SignUp(string email, string password, string firsName, string lastName);
         Task SignIn(string email, string password);
    }
}