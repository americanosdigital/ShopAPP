using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPP.Application.DTOs.Customers;

namespace ShopAPP.Application.Interfaces.Customers
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResponseDto>> GetAllAsync();
        Task<CustomerResponseDto?> GetByIdAsync(Guid id);
        Task<CustomerResponseDto> CreateAsync(CustomerCreateDto dto);
        Task UpdateAsync(CustomerUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
