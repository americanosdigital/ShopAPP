using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPP.Application.DTOs.OrderDetail;

namespace ShopAPP.Application.DTOs.Orders
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        
        public List<OrderDetailDto> Items { get; set; } = new();

        public decimal TotalAmount { get; set; }
    }

}
