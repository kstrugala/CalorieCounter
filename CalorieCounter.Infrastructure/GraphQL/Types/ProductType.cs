using CalorieCounter.Core.Domain;
using GraphQL.Types;

namespace CalorieCounter.Infrastructure.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            Field(x=>x.Id, type: typeof(IdGraphType)).Description("Id of the product.");
            Field(x=>x.Name).Description("Name of the product.");
            Field(x=>x.Kcal);
            Field(x=>x.Carbohydrates);
            Field(x=>x.Fats);
            Field(x=>x.ServeSize).Description("Standard serve size.");
        }
    }
}