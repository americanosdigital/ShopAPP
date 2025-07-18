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
    public static class CustomerSeeder
    {
        public static async Task<List<Customer>> SeedAsync(ShopAppDbContext context)
        {
            if (context.Customers.Any())
                return context.Customers.ToList();

            var customerFaker = new Faker<Customer>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.FullName, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber().Substring(0, 15))
                .RuleFor(c => c.ImageUrl, f => f.Internet.Avatar())
                .RuleFor(c => c.CreatedAt, _ => DateTime.UtcNow)
                .RuleFor(c => c.UpdatedAt, _ => DateTime.UtcNow);

            var customers = customerFaker.Generate(5);

            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();

            return customers;
        }
    }
}
