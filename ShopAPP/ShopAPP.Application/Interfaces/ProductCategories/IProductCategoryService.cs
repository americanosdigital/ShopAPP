using ShopAPP.Application.DTOs.ProductCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPP.Application.Interfaces.ProductCategories
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDto>> GetAllAsync();
        Task<ProductCategoryDto?> GetByIdAsync(Guid id);
        Task<ProductCategoryDto> CreateAsync(ProductCategoryCreateDto dto); 
        Task UpdateAsync(ProductCategoryUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
