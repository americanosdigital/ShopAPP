using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopAPP.Application.DTOs.ProductCategories;
using ShopAPP.Domain.Entities;

namespace ShopAPP.Application.Mappings.ProductCategories
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<ProductCategoryCreateDto, ProductCategory>();
            CreateMap<ProductCategoryUpdateDto, ProductCategory>();
        }
    }
}
