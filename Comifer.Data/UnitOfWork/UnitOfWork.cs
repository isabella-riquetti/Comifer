using Comifer.Data.Context;
using Comifer.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ComiferContext _context = new ComiferContext();

        public UnitOfWork()
        {
            _context.Configuration.LazyLoadingEnabled = true;

            Brand = new BrandRepository(_context);
            Category = new CategoryRepository(_context);
            Customer = new CustomerRepository(_context);
            CustomerAddress = new CustomerAddressRepository(_context);
            File = new FileRepository(_context);
            Product = new ProductRepository(_context);
            Promotion = new PromotionRepository(_context);
            ProductParent = new ProductParentRepository(_context);
            Provider = new ProviderRepository(_context);
        }

        public IBrandRepository Brand { get; set; }
        public ICategoryRepository Category { get; set; }
        public ICustomerRepository Customer { get; set; }
        public ICustomerAddressRepository CustomerAddress { get; set; }
        public IFileRepository File { get; set; }
        public IProductRepository Product { get; set; }
        public IPromotionRepository Promotion { get; set; }
        public IProductParentRepository ProductParent { get; set; }
        public IProviderRepository Provider { get; set; }


        private bool _disposed;
        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            Clear(true);
            GC.SuppressFinalize(this);
        }
        private void Clear(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        ~UnitOfWork()
        {
            Clear(false);
        }
    }
}
