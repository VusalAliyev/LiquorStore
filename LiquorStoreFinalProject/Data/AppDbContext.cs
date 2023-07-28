﻿using LiquorStoreFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LiquorStoreFinalProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductImage)
                .WithOne(pi => pi.Product)
                .HasForeignKey<Product>(p => p.ProductImageId);
        }

    }
}
