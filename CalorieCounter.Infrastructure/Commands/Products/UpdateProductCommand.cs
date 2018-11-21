using System;
using CalorieCounter.Core.Domain;
using CalorieCounter.Infrastructure.DTO;
using Microsoft.AspNetCore.JsonPatch;

namespace CalorieCounter.Infrastructure.Commands.Products
{
    public class UpdateProductCommand : ICommand
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<ProductDto> ProductPatch {get; set; }
    }
}