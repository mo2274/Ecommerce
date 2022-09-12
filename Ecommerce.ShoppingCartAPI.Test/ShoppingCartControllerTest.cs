using Ecommerce.ShoppingCartAPI.Controllers;
using Ecommerce.ShoppingCartAPI.Repositories;
using FakeItEasy;
using NUnit.Framework;

namespace Ecommerce.ShoppingCartAPI.Test
{
    public class ShoppingCartControllerTest
    {
        ShoppingCartController controller = null;

        [SetUp]
        public void Setup()
        {
            var shoppingCartRepository = A.Fake<IShoppingCartRepository>();
            controller = new ShoppingCartController(shoppingCartRepository);
        }

        [Test]
        public void AddItemToNonExistingShoppingCart()
        {
            Assert.Pass();
        }

        [Test]
        public void AddItemToExistingShoppingCart()
        {
            Assert.Pass();
        }

        [Test]
        public void AddExistingItemToExistingShoppingCart()
        {
            Assert.Pass();
        }
    }
}