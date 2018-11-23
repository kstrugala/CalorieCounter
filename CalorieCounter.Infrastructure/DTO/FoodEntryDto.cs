using System;

namespace CalorieCounter.Infrastructure.DTO
{
    public class FoodEntryDto
    {
        public DateTime Date { get; set; }
        public ProductDto Product { get; set; }

    }
}