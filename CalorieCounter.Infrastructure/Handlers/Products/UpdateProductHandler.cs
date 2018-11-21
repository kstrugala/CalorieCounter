using System.Threading.Tasks;
using CalorieCounter.Infrastructure.Commands;
using CalorieCounter.Infrastructure.Commands.Products;
using CalorieCounter.Infrastructure.Exceptions;
using CalorieCounter.Infrastructure.Services;

namespace CalorieCounter.Infrastructure.Handlers.Products
{
    public class UpdateProductHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductService _productService;

        public UpdateProductHandler(IProductService productService)
        {
            _productService=productService;
        }

        public async Task HandleAsync(UpdateProductCommand command)
        {
            var product = await _productService.GetProductAsync(command.Id);
            if(product==null)
            {
                throw new ServiceException(ErrorCodes.InvalidId, $"Product with id: {command.Id} doesn't exist.");
            }

            command.ProductPatch.ApplyTo(product);

            await _productService.UpdateProductAsync(product);

        }
    }
}