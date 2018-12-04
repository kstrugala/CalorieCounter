using CalorieCounter.Core.Domain;
using GraphQL.Types;

namespace CalorieCounter.Infrastructure.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            Field(x=>x.Id, type: typeof(IdGraphType));
            Field(x=>x.Name);
            Field(x=>x.Kcal);
            Field(x=>x.Carbohydrates);
            Field(x=>x.Fats);
            Field(x=>x.ServeSize);
        }
    }
}