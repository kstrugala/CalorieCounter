using System;

namespace CalorieCounter.Infrastructure.DTO
{
    public class ProductDto
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