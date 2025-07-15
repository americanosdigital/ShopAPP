using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopAPP.Domain.Entities;
using ShopAPP.Infrastructure.Data.Mappings;
using ShopAPP.Infrastructure.Identity.Models;
using ShopAPP.Infrastructure.Identity.Mappings; // Adicione isso

namespace ShopAPP.Infrastructure.Data.DataContext
{
    public class ShopAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public ShopAppDbContext(DbContextOptions<ShopAppDbContext> options)
            : base(options) { }

        // DbSets
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetail => Set<OrderDetail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeamentos do domínio
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ProductCategoryMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderDetailMap());

            // 👇 Adicione o mapeamento personalizado do Identity (ApplicationUser)
            modelBuilder.ApplyConfiguration(new ApplicationUserMap());
        }
    }
}
