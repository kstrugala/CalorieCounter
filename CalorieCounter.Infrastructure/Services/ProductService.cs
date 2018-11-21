using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CalorieCounter.Core.Domain;
using CalorieCounter.Core.Queries;
using CalorieCounter.Infrastructure.DTO;
using CalorieCounter.Infrastructure.EF;
using CalorieCounter.Infrastructure.Helpers.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounter.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly CalorieCounterContext _context;
        private readonly IMapper _mapper;

        public ProductService(CalorieCounterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ProductDto> GetProductAsync(Guid id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x=>x.Id==id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<PagedResult<ProductDto>> GetProductsPagedAsync(ProductQuery query)
        {
            var page = query.Page;
            var pageSize = query.PageSize;

            // Filter

            var linqQuery = _context.Products
                                .Where(x => x.Name.Contains(query.Name));


            var count = await linqQuery.CountAsync();

            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (page < 1) page = 1;

            if (totalPages == 0) totalPages = 1;

            if (page > totalPages) page = totalPages;

            var results = await linqQuery
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            var resultsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(results);

            return new PagedResult<ProductDto>(resultsDto, count, page, pageSize, totalPages);
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductDto product)
        {
            var p = await _context.Products.SingleOrDefaultAsync(x=>x.Id==product.Id);
            
            p.SetName(product.Name);
            p.SetCarbohydrates(product.Carbohydrates);
            p.SetFats(product.Fats);
            p.SetKcal(product.Kcal);
            p.SetProtiens(product.Proteins);
            p.SetServeSize(product.ServeSize);

            await UpdateProductAsync(p);
        }
    }
}