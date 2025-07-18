using Bogus;
using Microsoft.EntityFrameworkCore;
using ShopAPP.Domain.Entities;
using ShopAPP.Infrastructure.Data.DataContext;
using ShopAPP.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPP.Infrastructure.Data.Seeders
{
    public static class OrderSeeder
    {
        public static async Task<List<Order>> SeedAsync(ShopAppDbContext context, List<Customer> customers, List<Product> products)
        {
            if (context.Orders.Any())
                return context.Orders.ToList();

            var orders = new List<Order>();
            var orderDetails = new List<OrderDetail>();
            var faker = new Faker();

            foreach (var customer in customers)
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customer.Id,
                    OrderDate = DateTime.UtcNow,
                    Total = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Items = new List<OrderDetail>()
                };

                var selectedProducts = faker.PickRandom(products, 2).ToList();

                foreach (var product in selectedProducts)
                {
                    var quantity = faker.Random.Int(1, 3);
                    var detail = new OrderDetail
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Quantity = quantity,
                        UnitPrice = product.Price,
                        ProductImageUrl = product.ImageUrl,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    order.Items.Add(detail);
                    order.Total += quantity * product.Price;
                    orderDetails.Add(detail);
                }

                orders.Add(order);
            }

            await context.Orders.AddRangeAsync(orders);
            await context.OrderDetail.AddRangeAsync(orderDetails);
            await context.SaveChangesAsync();

            return orders;
        }
    }
}
