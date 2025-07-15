using AutoMapper;
using ShopAPP.Application.DTOs.Orders;
using ShopAPP.Application.DTOs.ProductCategories;
using ShopAPP.Application.Interfaces.Orders;
using ShopAPP.Domain.Entities;
using ShopAPP.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPP.Application.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _uow.Orders.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            var order = await _uow.Orders.GetByIdAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateAsync(OrderCreateDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.OrderDate = DateTime.UtcNow;
            order.Total = order.Items.Sum(i => i.Quantity * i.UnitPrice);

            await _uow.Orders.AddAsync(order);
            await _uow.CommitAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task UpdateAsync(OrderUpdateDto dto)
        {
            var entity = await _uow.Orders.GetByIdAsync(dto.Id);
            if (entity == null) return;

            _mapper.Map(dto, entity);
            await _uow.Orders.UpdateAsync(entity);
            await _uow.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _uow.Orders.GetByIdAsync(id);
            if (order == null) return;

            _uow.Orders.RemoveAsync(order);
            await _uow.CommitAsync();
        }
    }

}
