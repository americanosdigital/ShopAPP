using ShopAPP.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPP.Application.DTOs.ProductCategories
{
    public class ProductCategoryDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public List<ProductDto>? Products { get; set; }
    }
}
