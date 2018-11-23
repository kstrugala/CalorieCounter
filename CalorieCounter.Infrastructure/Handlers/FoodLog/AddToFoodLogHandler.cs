using System.Threading.Tasks;
using CalorieCounter.Infrastructure.Commands;
using CalorieCounter.Infrastructure.Commands.FoodLog;
using CalorieCounter.Infrastructure.Services;

namespace CalorieCounter.Infrastructure.Handlers.FoodLog
{
    public class AddToFoodLogHandler : ICommandHandler<AddToFoodLogCommand>
    {
        private readonly IFoodLogService _foodLogService;
        public AddToFoodLogHandler(IFoodLogService foodLogService)
        {
            _foodLogService=foodLogService;
        }

        public async Task HandleAsync(AddToFoodLogCommand command)
        {
            foreach(var e in command.FoodEntries)
            {
                await _foodLogService.AddToFoodLog(command.UserId, e.ProductId, e.Quantity);
            }
        }
    }
}