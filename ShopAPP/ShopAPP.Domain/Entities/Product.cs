using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPP.Domain.Common;

namespace ShopAPP.Domain.Entities
{
    public class Product : BaseEntity
    {        
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public Guid ProductCategoryId { get; set; }
        public ProductCategory? Category { get; set; }
        public string? ImageUrl { get; set; }        
    }

}
