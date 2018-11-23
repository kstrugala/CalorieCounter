using System;

namespace CalorieCounter.Core.Domain
{
    public class FoodEntry
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public FoodLog FoodLog { get; set; }

        protected FoodEntry()
        {

        }

        public FoodEntry(DateTime date, Guid productId, int quantity, FoodLog foodLog)
        {
            Id=Guid.NewGuid();
            Date=date;
            ProductId=productId;
            Quantity=quantity;
            FoodLog = foodLog;
        }

    }
}