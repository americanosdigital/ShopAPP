using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopAPP.Domain.Entities;
using ShopAPP.Domain.Interfaces;
using ShopAPP.Infrastructure.Data.DataContext;

namespace ShopAPP.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopAppDbContext _context;

        public ProductRepository(ShopAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Product product)
        {
            _context.Products.Remove(product);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId) =>
            await _context.Products.Where(p => p.ProductCategoryId == categoryId).ToListAsync();

    }
}
