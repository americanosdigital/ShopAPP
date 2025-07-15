using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPP.Application.DTOs.OrderDetail
{
    public class OrderDetailUpdateDto
    {
        public Guid Id { get; set; } 
        public Guid ProductId { get; set; } 
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
