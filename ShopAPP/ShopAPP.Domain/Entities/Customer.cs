using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPP.Domain.Common;

namespace ShopAPP.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}