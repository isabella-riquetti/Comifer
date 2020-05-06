using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comifer.ADM.Services
{
    public class ProductParentService : IProductParentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public ProductParentService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public List<DetailedProductParentViewModel> GetAll(Guid? brandId, Guid? categoryId)
        {
            var productParents = _unitOfWork.ProductParent
                .Get(b => (brandId == null || b.BrandId == brandId)
                && (categoryId == null || b.CategoryId == categoryId))
                .Select(p => new DetailedProductParentViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    BrandId = p.BrandId,
                    Brand = p.Brand,
                    CategoryId = p.CategoryId,
                    Category = p.Category,
                    Products = p.Products
                })
                .ToList();
            return productParents;
        }

        public DetailedProductParentViewModel GetDetailed(Guid id)
        {
            var productParent = _unitOfWork.ProductParent.Get(b => b.Id == id)
                .Select(p => new DetailedProductParentViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    BrandId = p.BrandId,
                    Brand = p.Brand,
                    CategoryId = p.CategoryId,
                    Category = p.Category,
                    Products = p.Products
                })
                .FirstOrDefault();

            productParent.FilesInfo = _fileService.GetFileInfoByReferId(productParent.Id);
            return productParent;
        }

        public ProductParentEditViewModel GetWithFiles(Guid id)
        {
            var productParent = _unitOfWork.ProductParent.Get(b => b.Id == id)
                .Select(p => new ProductParentEditViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    BrandId = p.BrandId,
                    Brand = p.Brand,
                    CategoryId = p.CategoryId,
                    Category = p.Category
                })
                .FirstOrDefault();

            var files = _fileService.GetFileInfoByReferId(productParent.Id);
            productParent.FilesInfo = files;

            return productParent;
        }

        public ProductParent Get(Guid id)
        {
            var productParent = _unitOfWork.ProductParent.FirstOrDefault(b => b.Id == id);
            return productParent;
        }

        public NotificationViewModel Edit(ProductParentEditViewModel product)
        {
            var existingProductParent = _unitOfWork.ProductParent.Get(p => p.Id == product.Id).FirstOrDefault();
            existingProductParent.Name = product.Name;
            existingProductParent.Code = product.Code;
            existingProductParent.CategoryId = product.CategoryId;
            existingProductParent.BrandId = product.BrandId;

            _unitOfWork.ProductParent.Edit(existingProductParent);
            _unitOfWork.Commit();

            _fileService.UploadFiles(product.Files, existingProductParent.Id, "Product");
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Vista Explodida editada com sucesso."
            };
        }

        public NotificationViewModel Create(ProductParentViewModel product)
        {
            var newProductParent = new ProductParent()
            {
                Id = Guid.NewGuid(),
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Code = product.Code                
            };
            _unitOfWork.ProductParent.Add(newProductParent);
            _unitOfWork.Commit();
            _fileService.UploadFiles(product.Files, newProductParent.Id, "Product");

            var result = new NotificationViewModel()
            {
                Message = "Vista Explodida inclusa com sucesso.",
                Status = true,
                Title = "Sucesso!"
            };
            return result;
        }

        public List<SelectListItem> GetSelectList()
        {
            var result = _unitOfWork.ProductParent.Get().ToSelectList(p => p.Id.ToString(), p => p.Name);
            return result;
        }

        public List<SelectListItem> GetSelectListWithAll()
        {
            var result = _unitOfWork.ProductParent.Get().ToSelectListAndAll(p => p.Id.ToString(), p => p.Name);
            return result;
        }
    }
}
