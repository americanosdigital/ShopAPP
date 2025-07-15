using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopAPP.Application.DTOs.ProductCategories;
using ShopAPP.Application.DTOs.Products;
using ShopAPP.Application.Interfaces.ProductCategories;
using ShopAPP.Domain.Entities;
using ShopAPP.Domain.Interfaces;
using ShopAPP.Infrastructure.UnitOfWork;

namespace ShopAPP.Application.Services.ProductCategories
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductCategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetAllAsync()
        {
            var categories = await _uow.ProductCategories.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductCategoryDto>>(categories);
        }

        public async Task<ProductCategoryDto?> GetByIdAsync(Guid id)
        {
            var entity = await _uow.ProductCategories.GetByIdAsync(id);
            if (entity == null) return null;

            return _mapper.Map<ProductCategoryDto>(entity);
        }

        public async Task<ProductCategoryDto> CreateAsync(ProductCategoryCreateDto dto)
        {
            var entity = _mapper.Map<ProductCategory>(dto);
            entity.Id = Guid.NewGuid(); // opcional se não for gerado automaticamente
            await _uow.ProductCategories.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<ProductCategoryDto>(entity); // ✅ mapeia corretamente o retorno
        }

        public async Task UpdateAsync(ProductCategoryUpdateDto dto)
        {
            var entity = await _uow.ProductCategories.GetByIdAsync(dto.Id);
            if (entity == null) return;

            _mapper.Map(dto, entity); 
            await _uow.ProductCategories.UpdateAsync(entity);
            await _uow.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _uow.ProductCategories.GetByIdAsync(id);
            if (entity == null) return;

            await _uow.ProductCategories.RemoveAsync(entity);
            await _uow.CommitAsync();
        }
    }

}
