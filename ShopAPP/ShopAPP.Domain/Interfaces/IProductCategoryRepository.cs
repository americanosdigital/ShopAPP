using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPP.Domain.Common;
using ShopAPP.Domain.Entities;

namespace ShopAPP.Domain.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync();

        Task<ProductCategory?> GetByIdAsync(Guid id);

        Task AddAsync(ProductCategory category);

        Task UpdateAsync(ProductCategory category);

        Task RemoveAsync(ProductCategory category);
    }
}
