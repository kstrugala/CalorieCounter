using System.Threading.Tasks;

namespace CalorieCounter.Infrastructure.Commands
{
    public interface ICommandDispatcher
    {
         Task DispatchAsync<T>(T command) where T: ICommand;
    }
}