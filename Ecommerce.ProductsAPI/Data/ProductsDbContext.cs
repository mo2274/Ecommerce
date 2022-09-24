using Ecommerce.ProductsAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductsAPI.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Id = 1,
                Name = "Pen",
                Price = 10,
                Cost = 20,
                ImageUrl = ""
            });
        }
    }


}
