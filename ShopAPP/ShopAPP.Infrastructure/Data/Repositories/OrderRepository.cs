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
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopAppDbContext _context;

        public OrderRepository(ShopAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
             .Include(o => o.Items)
                 .ThenInclude(i => i.Product)
             .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders
               .Include(o => o.Items)
                   .ThenInclude(i => i.Product) 
               .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            order.Id = Guid.NewGuid(); // Garante que a Order tenha Id
            foreach (var item in order.Items)
            {
                item.Id = Guid.NewGuid();
                item.OrderId = order.Id;
            }

            order.Total = order.Items.Sum(i => i.Quantity * i.UnitPrice);

            await _context.Orders.AddAsync(order);
        }


        public async Task UpdateAsync(Order order)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == order.Id);

            if (existingOrder == null)
                return;

            existingOrder.CustomerId = order.CustomerId;
            existingOrder.OrderDate = order.OrderDate;

            // Remoção
            foreach (var existingItem in existingOrder.Items.ToList())
            {
                if (!order.Items.Any(i => i.Id == existingItem.Id))
                {
                    _context.OrderDetail.Remove(existingItem);
                }
            }

            foreach (var item in order.Items)
            {
                var existingItem = existingOrder.Items.FirstOrDefault(i => i.Id == item.Id);

                if (existingItem != null)
                {
                    existingItem.ProductId = item.ProductId;
                    existingItem.Quantity = item.Quantity;
                    existingItem.UnitPrice = item.UnitPrice;
                }
                else
                {
                    item.Id = Guid.NewGuid(); 
                    item.OrderId = order.Id;
                    _context.OrderDetail.Add(item);
                }
            }            
            existingOrder.Total = existingOrder.Items.Sum(i => i.Quantity * i.UnitPrice);
        }

        public async Task RemoveAsync(Order order)
        {
            // Carrega os OrderDetails explicitamente se necessário
            var existingOrder = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == order.Id);

            if (existingOrder != null)
            {
                // Remove os itens antes de remover o pedido
                _context.OrderDetail.RemoveRange(existingOrder.Items);
                _context.Orders.Remove(existingOrder);
            }
        }

        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId) =>
            await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
    }
}
