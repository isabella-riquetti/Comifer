using Comifer.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBrandRepository Brand { get; set; }
        ICategoryRepository Category { get; set; }
        ICustomerRepository Customer { get; set; }
        ICustomerAddressRepository CustomerAddress { get; set; }
        IFileRepository File { get; set; }
        IProductRepository Product { get; set; }
        IPromotionRepository Promotion { get; set; }
        IProductParentRepository ProductParent { get; set; }
        IProviderRepository Provider { get; set; }

        void Commit();
    }
}
