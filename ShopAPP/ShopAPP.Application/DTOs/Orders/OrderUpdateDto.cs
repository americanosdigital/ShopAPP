using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPP.Application.DTOs.OrderDetail;

namespace ShopAPP.Application.DTOs.Orders
{
    public class OrderUpdateDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderDetailUpdateDto> Items { get; set; } = new();
    }
}
