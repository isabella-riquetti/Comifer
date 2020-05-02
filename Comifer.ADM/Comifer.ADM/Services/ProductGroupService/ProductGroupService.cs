using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comifer.ADM.Services
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ProductGroup> GetAll()
        {
            var productGroups = _unitOfWork.ProductGroup.Get().ToList();
            return productGroups;
        }

        public ProductGroup Get(Guid id)
        {
            var productGroup = _unitOfWork.ProductGroup.FirstOrDefault(b => b.Id == id);
            return productGroup;
        }

        public NotificationViewModel Edit(ProductGroup productGroup)
        {
            _unitOfWork.ProductGroup.Edit(productGroup);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Compatibilidade editada com sucesso."
            };
        }

        public NotificationViewModel Create(ProductGroup productGroup)
        {
            productGroup.Id = Guid.NewGuid();
            _unitOfWork.ProductGroup.Add(productGroup);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Compatibilidade criada com sucesso."
            };
        }
    }
}
