using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShopAPP.Application.DTOs.Products
{
    public class ProductCreateDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid ProductCategoryId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrlString { get; set; }
    }
}
