using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopAPP.Application.DTOs.Products;
using ShopAPP.Domain.Entities;

namespace ShopAPP.Application.Mappings.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ImageUrlString, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
                .ForMember(dest => dest.ImageUrlString, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.ProductCategoryId, opt => opt.MapFrom(src => src.ProductCategoryId));

            CreateMap<ProductUpdateDto, Product>();
        }
    }

}
