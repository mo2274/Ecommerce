using Ecommerce.ShoppingCartAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ShoppingCartAPI.Data
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : base(options)
        { }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
