using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ShopAPP.Infrastructure.Data.DataContext;

namespace ShopAPP.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ShopAppDbContext>
    {
        public ShopAppDbContext CreateDbContext(string[] args)
        {
            // Caminho para appsettings.json da API (projeto de startup)
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../ShopAPP.API");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ShopAppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new ShopAppDbContext(optionsBuilder.Options);
        }
    }
}
