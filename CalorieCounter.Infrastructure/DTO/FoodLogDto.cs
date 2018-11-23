using System.Collections.Generic;

namespace CalorieCounter.Infrastructure.DTO
{
    public class FoodLogDto
    {
        public ICollection<FoodEntryDto> FoodEntries { get; set; }
    }
}