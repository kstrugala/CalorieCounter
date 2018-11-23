using System;
using System.Collections.Generic;

namespace CalorieCounter.Core.Domain
{
    public class FoodLog
    {
        public Guid UserId { get; set; }
        public ICollection<FoodEntry> Products {get; set;}

        protected FoodLog()
        {

        }

        public FoodLog(Guid userId)
        {
            UserId=userId;
        }
    }
}