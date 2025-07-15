using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPP.Application.DTOs.Orders;

namespace ShopAPP.Application.Interfaces.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(Guid id);
        Task<OrderDto> CreateAsync(OrderCreateDto dto);
        Task UpdateAsync(OrderUpdateDto dto);
        Task DeleteAsync(Guid id);
    }

}
