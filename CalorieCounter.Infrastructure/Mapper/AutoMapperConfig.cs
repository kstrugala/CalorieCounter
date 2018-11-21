using AutoMapper;
using CalorieCounter.Core.Domain;
using CalorieCounter.Infrastructure.DTO;

namespace CalorieCounter.Infrastructure.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Product, ProductDto>();

            }).CreateMapper();
    }
}