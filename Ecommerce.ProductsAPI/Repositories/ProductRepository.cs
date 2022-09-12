using AutoMapper;
using Ecommerce.ProductsAPI.Data;
using Ecommerce.ProductsAPI.Data.Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductsAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsDbContext context;
        private readonly IMapper mapper;

        public ProductRepository(ProductsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            return mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await context.Products.ToListAsync();
            return mapper.Map<List<ProductDto>>(products);
        }
    }
}
