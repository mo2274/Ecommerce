using Ecommerce.ProductsAPI.Data.Entities.Dtos;

namespace Ecommerce.ProductsAPI.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
    }
}
