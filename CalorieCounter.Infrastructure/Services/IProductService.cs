using System;
using System.Threading.Tasks;
using CalorieCounter.Core.Domain;
using CalorieCounter.Core.Queries;
using CalorieCounter.Infrastructure.DTO;
using CalorieCounter.Infrastructure.Helpers.Pagination;

namespace CalorieCounter.Infrastructure.Services
{
    public interface IProductService : IService
    {
         Task<ProductDto> GetProductAsync(Guid id);
         Task<PagedResult<ProductDto>> GetProductsPagedAsync(ProductQuery query);
         Task AddProductAsync(Product product);
         Task UpdateProductAsync(Product product);
         Task UpdateProductAsync(ProductDto product);

    }
}