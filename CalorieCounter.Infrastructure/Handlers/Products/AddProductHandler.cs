using System.Threading.Tasks;
using CalorieCounter.Core.Domain;
using CalorieCounter.Infrastructure.Commands;
using CalorieCounter.Infrastructure.Commands.Products;
using CalorieCounter.Infrastructure.Services;

namespace CalorieCounter.Infrastructure.Handlers.Products
{
    public class AddProductHandler : ICommandHandler<AddProductCommand>
    {
        private readonly IProductService _productService;

        public AddProductHandler(IProductService productService)
        {
            _productService=productService;
        }

        public async Task HandleAsync(AddProductCommand command)
        {
            var product = new Product(command.Id, command.Name, command.Kcal, command.Carbohydrates, command.Proteins, command.Fats, command.ServeSize);
            await _productService.AddProductAsync(product);
        }
    }
}