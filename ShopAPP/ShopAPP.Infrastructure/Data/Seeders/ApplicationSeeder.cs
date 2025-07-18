using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bogus;
using ShopAPP.Domain.Entities;
using ShopAPP.Infrastructure.Data.DataContext;
using ShopAPP.Infrastructure.Identity;


namespace ShopAPP.Infrastructure.Data.Seeders
{
    public static class ApplicationSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ShopAppDbContext>();

            // Aplica migrations pendentes
            await context.Database.MigrateAsync();

            // Executa os seeders na ordem correta e aguarda os resultados
            var customers = await CustomerSeeder.SeedAsync(context);
            var categories = await ProductCategorySeeder.SeedAsync(context);
            var products = await ProductSeeder.SeedAsync(context, categories);
            var orders = await OrderSeeder.SeedAsync(context, customers, products);

            // Salva todas as alterações no banco
            await context.SaveChangesAsync();
        }
    }
}
