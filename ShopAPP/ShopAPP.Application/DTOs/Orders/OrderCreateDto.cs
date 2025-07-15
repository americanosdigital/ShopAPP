using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPP.Application.DTOs.OrderDetail;

namespace ShopAPP.Application.DTOs.Orders
{
    public class OrderCreateDto
    {
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<OrderDetailCreateDto> Items { get; set; } = new();
    }
}
