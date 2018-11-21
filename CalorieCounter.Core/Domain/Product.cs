using System;

namespace CalorieCounter.Core.Domain
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Kcal { get; set; }
        public double Carbohydrates { get; private set; }
        public double Proteins {get; private set; }
        public double Fats { get; private set; }
        public int ServeSize { get; private set; }

        protected Product()
        {

        }

        public Product(Guid id, string name, int kcal, double carbohydrates, double proteins, double fats, int serveSize)
        {
            Id = id;
            SetName(name);
            SetKcal(kcal);
            SetCarbohydrates(carbohydrates);
            SetProtiens(proteins);
            SetFats(fats);
            SetServeSize(serveSize);
        }

        public void SetName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"Name cannot be empty.");
            }
            Name=name;
        }

        public void SetKcal(int kcal)
        {
            if(kcal <= 0)
            {
                throw new Exception($"Kcal cannot be negative number.");
            }
            Kcal=kcal;
        }

        public void SetCarbohydrates(double carbohydrates)
        {
            if(carbohydrates <= 0)
            {
                throw new Exception($"Carobhydrates cannot be negative number.");
            }
            Carbohydrates=carbohydrates;
        }
        public void SetProtiens(double proteins)
        {
            if(proteins <= 0)
            {
                throw new Exception($"Proteins cannot be negative number.");
            }
            Proteins=proteins;
        }
        
        public void SetFats(double fats)
        {
            if(fats <= 0)
            {
                throw new Exception($"Fats cannot be negative number.");
            }
            Fats=fats;
        }
        
        public void SetServeSize(int serveSize)
        {
            if(serveSize <= 0)
            {
                throw new Exception($"ServeSize cannot be negative number.");
            }
            ServeSize=serveSize;
        }
    }
}