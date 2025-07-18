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
        public Product() { }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public Guid ProductCategoryId { get; set; }
        public ProductCategory? Category { get; set; }

        public Product(string name, string description, decimal price, string? imageUrl = null)
        {
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
        }

        public void Update(string name, string description, decimal price, string? imageUrl = null)
        {
            Name = name;
            Description = description;
            Price = price;

            if (!string.IsNullOrEmpty(imageUrl))
                ImageUrl = imageUrl;

            UpdateTimestamp();
        }
        public void Disable() => Deactivate();
    }
}