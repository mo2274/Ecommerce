﻿using Ecommerce.ProductsAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductsAPI.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
    }
}
