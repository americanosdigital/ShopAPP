using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPP.Domain.Common;
using ShopAPP.Domain.Entities;

namespace ShopAPP.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(Guid id);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product); 

        Task RemoveAsync(Product product);

        Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId);
    }
}
