using System;

namespace CalorieCounter.Infrastructure.Commands.FoodLog
{
    public class AddFoodEntry 
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}