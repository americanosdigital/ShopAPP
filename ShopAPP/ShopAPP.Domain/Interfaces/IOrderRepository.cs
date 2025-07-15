using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPP.Domain.Common;
using ShopAPP.Domain.Entities;

namespace ShopAPP.Domain.Interfaces
{
    public interface IOrderRepository
    {

        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(Guid id);

        Task AddAsync(Order order);

        Task UpdateAsync(Order order);

        Task RemoveAsync(Order order);

        Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId);
    }
}
