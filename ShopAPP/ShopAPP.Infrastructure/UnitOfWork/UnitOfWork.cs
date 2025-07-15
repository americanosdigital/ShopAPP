using System;
using System.Threading.Tasks;
using ShopAPP.Domain.Interfaces;
using ShopAPP.Infrastructure.Data.DataContext;
using ShopAPP.Infrastructure.Data.Repositories;

namespace ShopAPP.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ShopAppDbContext _context;

        private IProductRepository? _productRepository;
        private IProductCategoryRepository? _productCategoryRepository;
        private ICustomerRepository? _customerRepository;
        private IOrderRepository? _orderRepository;
        private IOrderDetailRepository? _orderDetailRepository;

        private bool _disposed = false;

        public UnitOfWork(ShopAppDbContext context)
        {
            _context = context;
        }

        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);

        public IProductCategoryRepository ProductCategories => _productCategoryRepository ??= new ProductCategoryRepository(_context);

        public ICustomerRepository Customers => _customerRepository ??= new CustomerRepository(_context);

        public IOrderRepository Orders => _orderRepository ??= new OrderRepository(_context);

        public IOrderDetailRepository OrderDetails => _orderDetailRepository ??= new OrderDetailRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

