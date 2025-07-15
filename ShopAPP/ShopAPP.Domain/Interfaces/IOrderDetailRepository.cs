using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPP.Domain.Entities;

namespace ShopAPP.Domain.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(Guid orderId);
        Task AddAsync(OrderDetail detail);
        void Remove(OrderDetail detail);
    }
}
