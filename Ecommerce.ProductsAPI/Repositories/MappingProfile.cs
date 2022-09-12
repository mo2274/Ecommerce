using AutoMapper;
using Ecommerce.ProductsAPI.Data.Entities;
using Ecommerce.ProductsAPI.Data.Entities.Dtos;

namespace Ecommerce.ProductsAPI.Repositories
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
