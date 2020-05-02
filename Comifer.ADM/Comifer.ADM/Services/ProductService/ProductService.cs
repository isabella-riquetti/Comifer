using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comifer.ADM.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Product> GetAll()
        {
            var categories = _unitOfWork.Product.Get().ToList();
            return categories;
        }

        public Product Get(Guid id)
        {
            var category = _unitOfWork.Product.FirstOrDefault(b => b.Id == id);
            return category;
        }

        public NotificationViewModel Edit(Product product)
        {
            _unitOfWork.Product.Edit(product);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Produto editada com sucesso."
            };
        }

        public NotificationViewModel Create(Product product)
        {
            product.Id = Guid.NewGuid();
            _unitOfWork.Product.Add(product);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Produto criada com sucesso."
            };
        }
    }
}
