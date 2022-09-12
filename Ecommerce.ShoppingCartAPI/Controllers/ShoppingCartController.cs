using Ecommerce.ShoppingCartAPI.Data.Entities;
using Ecommerce.ShoppingCartAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository shoppingCartRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        [HttpPost]
        public async Task AddItemToCart(string userName, Item item)
        {
            await shoppingCartRepository.AddItemToCartAsync(userName, item);
        }
    }
}
