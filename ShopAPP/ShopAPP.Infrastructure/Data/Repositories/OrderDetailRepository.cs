using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopAPP.Domain.Entities;
using ShopAPP.Domain.Interfaces;
using ShopAPP.Infrastructure.Data.DataContext;

namespace ShopAPP.Infrastructure.Data.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ShopAppDbContext _context;

        public OrderDetailRepository(ShopAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderDetail detail) => await _context.OrderDetail.AddAsync(detail);
        public void Remove(OrderDetail detail) => _context.OrderDetail.Remove(detail);
        public async Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(Guid orderId) =>
            await _context.OrderDetail.Where(d => d.ProductId == orderId).ToListAsync();
    }
}
