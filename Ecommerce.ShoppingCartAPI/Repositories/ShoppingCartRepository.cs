using Ecommerce.ShoppingCartAPI.Data;
using Ecommerce.ShoppingCartAPI.Data.Entities;

namespace Ecommerce.ShoppingCartAPI.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShoppingCartDbContext context;

        public ShoppingCartRepository(ShoppingCartDbContext context)
        {
            this.context = context;
        }
        public async Task AddItemToCartAsync(string userName, Item item)
        {
            var shoppingCart = context.ShoppingCarts
                .Where(s => s.Equals(userName) && s.IsOpened).FirstOrDefault();

            if (shoppingCart == null)
            {
                await CreateNewCartAsync(userName, item);
                return;
            }

            if (CartHasItem(shoppingCart, item.ProductName))
                UpdateItemCount(shoppingCart, item.ProductName);
            else
                AddItem(shoppingCart, item);

            context.ShoppingCarts.Update(shoppingCart);
            await context.SaveChangesAsync();
        }

        private void AddItem(ShoppingCart shoppingCart, Item item)
        {
            shoppingCart.Items.Add(item);
        }

        private void UpdateItemCount(ShoppingCart shoppingCart, string productName)
        {
            var item = shoppingCart.Items.Where(i => i.ProductName == productName).First();
            item.Count++;
        }

        private bool CartHasItem(ShoppingCart shoppingCart, string productName)
        {
            return shoppingCart.Items.Where(i => i.ProductName == productName).Any();
        }

        private async Task CreateNewCartAsync(string userName, Item item)
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.UserName = userName;
            shoppingCart.Items = new List<Item>() { item };
            shoppingCart.IsOpened = true;
            context.ShoppingCarts.Add(shoppingCart);
            await context.SaveChangesAsync();
        }
    }
}
