using System;

namespace CalorieCounter.Infrastructure.Commands.Products
{
    public class AddProductCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Kcal { get; set; }
        public double Carbohydrates { get;  set; }
        public double Proteins {get;  set; }
        public double Fats { get;  set; }
        public int ServeSize { get;  set; }
    }
}