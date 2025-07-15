using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopAPP.Domain.Common;
using ShopAPP.Domain.Entities;


namespace ShopAPP.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(Guid id);

        Task AddAsync(Customer customer);

        Task UpdateAsync(Customer customer);

        Task RemoveAsync(Customer customer);

        Task<Customer?> GetByEmailAsync(string email);
    }
}
