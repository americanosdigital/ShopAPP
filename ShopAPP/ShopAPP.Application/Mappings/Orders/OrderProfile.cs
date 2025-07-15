using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopAPP.Application.DTOs.Orders;
using ShopAPP.Domain.Entities;

namespace ShopAPP.Application.Mappings.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                 .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                 .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Total));

            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<OrderUpdateDto, Order>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
