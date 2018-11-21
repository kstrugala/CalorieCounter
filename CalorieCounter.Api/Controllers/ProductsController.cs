using System;
using System.Threading.Tasks;
using CalorieCounter.Core.Domain;
using CalorieCounter.Core.Queries;
using CalorieCounter.Infrastructure.Commands;
using CalorieCounter.Infrastructure.Commands.Products;
using CalorieCounter.Infrastructure.DTO;
using CalorieCounter.Infrastructure.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CalorieCounter.Api.Controllers
{
    [Route("/products/")]
    public class ProductsController : ApiControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(ICommandDispatcher commandDispatcher, IProductService productService) : base(commandDispatcher)
        {
            _productService=productService;
        }

        [HttpGet("{id}", Name="GetProduct")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            return Json(await _productService.GetProductAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQuery query)
        {
            var result = await _productService.GetProductsPagedAsync(query);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command)
        {
            command.Id=Guid.NewGuid();
            await CommandDispatcher.DispatchAsync<AddProductCommand>(command);

            var product = await _productService.GetProductAsync(command.Id);

            if(product!=null)
            {
                return CreatedAtRoute("GetProduct", new { id = command.Id }, product);
            }

            return StatusCode(500);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] JsonPatchDocument<ProductDto> productPatch)
        {
            UpdateProductCommand command = new UpdateProductCommand();
            command.Id=id;
            command.ProductPatch=productPatch;

            await CommandDispatcher.DispatchAsync<UpdateProductCommand>(command);
            
            var p = await _productService.GetProductAsync(id);
            
            return Ok(p);
        }

    }
}