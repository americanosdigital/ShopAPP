using AutoMapper;
using ShopAPP.Application.DTOs.Products;
using ShopAPP.Application.Interfaces;
using ShopAPP.Application.Interfaces.Products;
using ShopAPP.Domain.Entities;
using ShopAPP.Domain.Interfaces;

namespace ShopAPP.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        private const string ProductImagesPath = "wwwroot/uploads/products";

        public async Task<ProductDto> CreateAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            if (dto.ImageFile != null)
            {
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ImageFile.FileName)}";
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), ProductImagesPath, fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                product.ImageUrl = $"/uploads/products/{fileName}";
            }

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ProductDto>(product);
        }


        public async Task UpdateAsync(ProductUpdateDto dto)
        {
            var entity = await _unitOfWork.Products.GetByIdAsync(dto.Id);
            if (entity == null) return;

            _mapper.Map(dto, entity);

            if (dto.ImageFile != null)
            {
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ImageFile.FileName)}";
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), ProductImagesPath, fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                entity.ImageUrl = $"/uploads/products/{fileName}";
            }

            await _unitOfWork.Products.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }


        public async Task DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork.Products.GetByIdAsync(id);
            if (entity == null) return;

            _unitOfWork.Products.RemoveAsync(entity);
            await _unitOfWork.CommitAsync();
        }

    }

}
