using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comifer.ADM.Services
{
    public class ProductParentService : IProductParentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductParentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ProductParent> GetAll()
        {
            var productParents = _unitOfWork.ProductParent.Get().ToList();
            return productParents;
        }

        public ProductParent Get(Guid id)
        {
            var productParent = _unitOfWork.ProductParent.FirstOrDefault(b => b.Id == id);
            return productParent;
        }

        public NotificationViewModel Edit(ProductParent productParent)
        {
            _unitOfWork.ProductParent.Edit(productParent);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Máquina editada com sucesso."
            };
        }

        public NotificationViewModel Create(ProductParent productParent)
        {
            productParent.Id = Guid.NewGuid();
            _unitOfWork.ProductParent.Add(productParent);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Máquina criada com sucesso."
            };
        }
    }
}
