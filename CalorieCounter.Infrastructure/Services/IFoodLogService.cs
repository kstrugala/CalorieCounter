using System;
using System.Threading.Tasks;
using CalorieCounter.Infrastructure.DTO;
using CalorieCounter.Infrastructure.Helpers.Pagination;

namespace CalorieCounter.Infrastructure.Services
{
    public interface IFoodLogService : IService
    {
         Task<FoodLogDto> GetFoodLogAsync(Guid userId);
         Task AddToFoodLog(Guid userId, Guid productId, int quantity);
    }
}