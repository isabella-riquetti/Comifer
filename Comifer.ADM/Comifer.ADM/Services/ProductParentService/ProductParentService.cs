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

        public ProductParentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<DetailedProductParentViewModel> GetAll(Guid? brandId, Guid? categoryId)
        {
            var productParents = _unitOfWork.ProductParent
                .Get(b => (brandId == null || b.BrandId == brandId)
                && (categoryId == null || b.CategoryId == brandId))
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
            return productParent;
        }

        public ProductParentWithFileViewModel GetDetailedWithFiles(Guid id)
        {
            var productParent = _unitOfWork.ProductParent.Get(b => b.Id == id)
                .Select(p => new ProductParentWithFileViewModel()
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

            var files = GetFiles(productParent.Id);
            productParent.Files = files;

            return productParent;
        }

        private List<FileInfo> GetFiles(Guid id)
        {
            var fileInfos = new List<FileInfo>();
            var files = _unitOfWork.File.Get(i => i.ReferId == id).Select(i => new { i.FileName, i.MIME, i.FileBytes, i.Id }).ToList();
            foreach (var file in files)
            {
                fileInfos.Add(new FileInfo()
                {
                    Id = file.Id,
                    MIME = file.MIME,
                    FileName = file.FileName,
                    Base64File = Convert.ToBase64String(file.FileBytes)
                });
            }
            return fileInfos;
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

            var result = new NotificationViewModel()
            {
                Message = "Vista Explodida criada com sucesso.",
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
