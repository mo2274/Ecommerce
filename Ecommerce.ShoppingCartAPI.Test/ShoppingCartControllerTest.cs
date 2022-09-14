using Ecommerce.ShoppingCartAPI.Controllers;
using Ecommerce.ShoppingCartAPI.Data;
using Ecommerce.ShoppingCartAPI.Data.Entities;
using Ecommerce.ShoppingCartAPI.Repositories;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ShoppingCartAPI.Test
{
    public class ShoppingCartControllerTest
    {
        private static DbContextOptions<ShoppingCartDbContext> _contextOptions = 
            new DbContextOptionsBuilder<ShoppingCartDbContext>()
            .UseInMemoryDatabase("ShoppingCartDb")
            .Options;

        ShoppingCartDbContext context;
        ShoppingCartController controller;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new ShoppingCartDbContext(_contextOptions);
            controller = new ShoppingCartController(new ShoppingCartRepository(context));
            context.Database.EnsureCreated();
            SeedDataBase();
        }

        [Test]
        public async Task AddItemToNonExistingShoppingCart()
        {
            // arrange
            var userName = "New User";
            var item = new Item()
            {
                ProductName = "Product 3",
                Price = 50,
                Count = 1
            };

            // act
            await controller.AddItemToCart(userName, item);

            // assert
            var shoppingCart = await context.ShoppingCarts
                .Include(s => s.Items)
                .Where(s => s.UserName.Equals(userName))
                .SingleOrDefaultAsync();

            Assert.That(shoppingCart, Is.Not.Null);
            Assert.AreEqual(1, shoppingCart?.Items.Count);
        }

        [Test]
        public async Task AddItemToExistingShoppingCart()
        {
            // arrange
            var userName = "User 1";
            var productName = "Product 3";

            var item = new Item()
            {
                ProductName = productName,
                Price = 50,
                Count = 1
            };

            // act
            await controller.AddItemToCart(userName, item);

            // assert
            var shoppingCart = await context.ShoppingCarts
                .Include(s => s.Items)
                .Where(s => s.UserName.Equals(userName))
                .SingleOrDefaultAsync();

            Assert.That(shoppingCart, Is.Not.Null);
            Assert.AreEqual(1, shoppingCart?.Items.Where(i => i.ProductName == productName).ToList().Count);
        }

        [Test]
        public async Task AddExistingItemToExistingShoppingCart()
        {
            // arrange
            var userName = "User 2";
            var productName = "Product 2";

            var item = new Item()
            {
                ProductName = productName,
                Price = 50,
                Count = 2
            };

            // act
            await controller.AddItemToCart(userName, item);

            // assert
            var shoppingCart = await context.ShoppingCarts
                .Include(s => s.Items)
                .Where(s => s.UserName.Equals(userName))
                .SingleOrDefaultAsync();

            Assert.That(shoppingCart, Is.Not.Null);
            Assert.AreEqual(3, shoppingCart?.Items.Where(i => i.ProductName == productName).SingleOrDefault()?.Count);

        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDataBase()
        {
            var shoppingCart = new ShoppingCart()
            {
                UserName = "User 1",
                IsOpened = true,
                Items = new List<Item>()
                {
                    new Item()
                    {
                        ProductName = "Product 1",
                        Price = 12,
                        Count = 1
                    }
                }
            };

            var shoppingCart2 = new ShoppingCart()
            {
                UserName = "User 2",
                IsOpened = true,
                Items = new List<Item>()
                {
                    new Item()
                    {
                        ProductName = "Product 2",
                        Price = 12,
                        Count = 1
                    }
                }
            };

            context.ShoppingCarts.AddRange(shoppingCart, shoppingCart2);
            context.SaveChanges();
        }
    }
}