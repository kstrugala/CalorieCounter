using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalorieCounter.Core.Domain;
using CalorieCounter.Infrastructure.DTO;
using CalorieCounter.Infrastructure.EF;
using CalorieCounter.Infrastructure.Exceptions;
using CalorieCounter.Infrastructure.Helpers.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounter.Infrastructure.Services {
    public class FoodLogService : IFoodLogService {
        private readonly CalorieCounterContext _context;
        private readonly IProductService _productService;

        public FoodLogService (CalorieCounterContext context, IProductService productService) {
            _productService = productService;
            _context = context;
        }

        public async Task AddToFoodLog (Guid userId, Guid productId, int quantity) {
            var p = _productService.GetProductAsync (productId);

            if (p == null)
                throw new ServiceException (ErrorCodes.InvalidId, $"Product with id: {productId} doesn't exist.");

            if (quantity <= 0)
                throw new ServiceException (ErrorCodes.InvalidQuantity, "Quantity has to be positive number");

            var fl = await _context.FoodLogs.Include (i => i.Products).SingleOrDefaultAsync (x => x.UserId == userId);

            if (fl == null)
                throw new ServiceException (ErrorCodes.InvalidId, $"User with id: {userId} doesn't exist.");

            fl.Products.Add (new FoodEntry (DateTime.UtcNow, productId, quantity, fl));

            _context.Update (fl);

            await _context.SaveChangesAsync ();

        }

        public async Task<FoodLogDto> GetFoodLogAsync (Guid userId) {
            var fl = await _context.FoodLogs.Include(i=>i.Products).SingleOrDefaultAsync (x => x.UserId == userId);
            if (fl == null)
                throw new ServiceException (ErrorCodes.InvalidId, $"User with id: {userId} doesn't exist.");

            var flDto = new FoodLogDto ();
            flDto.FoodEntries = new List<FoodEntryDto> ();

            foreach (var p in fl.Products) {
                var f = new FoodEntryDto ();
                f.Date = p.Date;
                var product = await _productService.GetProductAsync (p.ProductId);
                f.Product = product;

                flDto.FoodEntries.Add (f);
            }

            return flDto;
        }
    }
}