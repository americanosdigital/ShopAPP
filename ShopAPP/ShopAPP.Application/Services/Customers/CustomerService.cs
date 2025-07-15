using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ShopAPP.Infrastructure.UnitOfWork;
using ShopAPP.Application.DTOs.Customers;
using ShopAPP.Application.Interfaces.Customers;
using ShopAPP.Domain.Entities;
using ShopAPP.Domain.Interfaces;


namespace ShopAPP.Application.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;        

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;            
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
        }

        public async Task<CustomerResponseDto?> GetByIdAsync(Guid id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            return _mapper.Map<CustomerResponseDto>(customer);
        }

        public async Task<CustomerResponseDto> CreateAsync(CustomerCreateDto dto)
        {
            var entity = _mapper.Map<Customer>(dto);
            entity.ImageUrl = dto.ImageUrl;

            await _unitOfWork.Customers.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CustomerResponseDto>(entity);
        }

        public async Task UpdateAsync(CustomerUpdateDto dto)
        {
            var entity = await _unitOfWork.Customers.GetByIdAsync(dto.Id);
            if (entity == null) return;

            _mapper.Map(dto, entity);
            if (!string.IsNullOrEmpty(dto.ImageUrl))
                entity.ImageUrl = dto.ImageUrl;

            await _unitOfWork.Customers.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork.Customers.GetByIdAsync(id);
            if (entity == null) return;

            _unitOfWork.Customers.RemoveAsync(entity);
            await _unitOfWork.CommitAsync();
        }    
 
    }

}
