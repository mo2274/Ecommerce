using Ecommerce.ShoppingCartAPI.Data.Entities;

namespace Ecommerce.ShoppingCartAPI.Repositories
{
    public interface IShoppingCartRepository
    {
        Task AddItemToCartAsync(string userName, Item item);
    }
}
