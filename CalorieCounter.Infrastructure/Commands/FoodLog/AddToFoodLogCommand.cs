using System;
using System.Collections.Generic;

namespace CalorieCounter.Infrastructure.Commands.FoodLog
{
    public class AddToFoodLogCommand : ICommand
    {
        public Guid UserId { get; set; }
        public IEnumerable<AddFoodEntry> FoodEntries { get; set; }
    }
}