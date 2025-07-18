using Bogus;
using Microsoft.EntityFrameworkCore;
using ShopAPP.Domain.Entities;
using ShopAPP.Infrastructure.Data.DataContext;
using ShopAPP.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPP.Infrastructure.Data.Seeders
{
    public static class ProductSeeder
    {
        public static async Task<List<Product>> SeedAsync(ShopAppDbContext context, List<ProductCategory> categories)
        {
            if (context.Products.Any())
                return context.Products.ToList();

            var productFaker = new Faker<Product>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => f.Random.Decimal(10, 500))
                .RuleFor(p => p.Stock, f => f.Random.Int(10, 100))
                .RuleFor(p => p.ImageUrl, f => f.Image.PicsumUrl())
                .RuleFor(p => p.ProductCategoryId, f => f.PickRandom(categories).Id)
                .RuleFor(p => p.CreatedAt, _ => DateTime.UtcNow)
                .RuleFor(p => p.UpdatedAt, _ => DateTime.UtcNow);

            var products = productFaker.Generate(10);

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();

            return products;
        }
    }
}
