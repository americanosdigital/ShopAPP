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
    public static class ProductCategorySeeder
    {
        public static async Task<List<ProductCategory>> SeedAsync(ShopAppDbContext context)
        {
            if (context.ProductCategories.Any())
                return context.ProductCategories.ToList();

            var categoryFaker = new Faker<ProductCategory>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                .RuleFor(c => c.CreatedAt, _ => DateTime.UtcNow)
                .RuleFor(c => c.UpdatedAt, _ => DateTime.UtcNow);

            var categories = categoryFaker.Generate(3);

            await context.ProductCategories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            return categories;
        }
    }
}
