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
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ShopAppDbContext _context;

        public ProductCategoryRepository(ShopAppDbContext context)
        {
            _context = context;
        }
                
        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory?> GetByIdAsync(Guid id)
        {
            return await _context.ProductCategories.FindAsync(id);
        }

        public async Task AddAsync(ProductCategory category)
        {
            await _context.ProductCategories.AddAsync(category);
        }

        public async Task UpdateAsync(ProductCategory category)
        {
            _context.ProductCategories.Update(category);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(ProductCategory category)
        {
            _context.ProductCategories.Remove(category);
            await Task.CompletedTask;
        }
    }
}
