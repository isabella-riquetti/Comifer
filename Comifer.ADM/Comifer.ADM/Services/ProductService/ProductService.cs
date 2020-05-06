using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comifer.ADM.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public ProductService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public List<DetailedProductViewModel> GetAll(Guid? productParentId, Guid? brandId)
        {
            var products = _unitOfWork.Product
                .Get(b => (brandId == null || b.BrandId == brandId)
                && (productParentId == null || b.ProductParentId == productParentId))
                .Select(p => new DetailedProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Price = p.Price,
                    Supply = p.Supply,
                    Weight = p.Weight,
                    ProductGroupId = p.ProductGroupId,
                    ProductGroup = p.ProductGroup,
                    BrandId = p.BrandId,
                    Brand = p.Brand,
                    ProductParentId = p.ProductParentId,
                    ProductParent = p.ProductParent
                })
                .ToList();

            return products;
        }

        public DetailedProductViewModel GetDetailed(Guid id)
        {
            var product = _unitOfWork.Product.Get(b => b.Id == id)
                .Select(p => new DetailedProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Price = p.Price,
                    Supply = p.Supply,
                    Weight = p.Weight,
                    ProductGroupId = p.ProductGroupId,
                    ProductGroup = p.ProductGroup,
                    BrandId = p.BrandId,
                    Brand = p.Brand,
                    ProductParentId = p.ProductParentId,
                    ProductParent = p.ProductParent
                })
                .FirstOrDefault();

            product.FilesInfo = _fileService.GetFileInfoByReferId(product.Id);
            return product;
        }

        public ProductEditViewModel Get(Guid id)
        {
            var product = _unitOfWork.Product.Get(b => b.Id == id)
                .Select(p => new ProductEditViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Price = p.Price,
                    Supply = p.Supply,
                    Weight = p.Weight,
                    ProductGroupId = p.ProductGroupId,
                    ProductGroup = p.ProductGroup,
                    BrandId = p.BrandId,
                    Brand = p.Brand,
                    ProductParentId = p.ProductParentId,
                    ProductParent = p.ProductParent
                })
                .FirstOrDefault();

            var files = _fileService.GetFileInfoByReferId(product.Id);
            product.FilesInfo = files;

            return product;
        }
        
        public NotificationViewModel Edit(ProductEditViewModel product)
        {
            var existingProduct = _unitOfWork.Product.Get(p => p.Id == product.Id).FirstOrDefault();
            existingProduct.Name = product.Name;
            existingProduct.Code = product.Code;
            existingProduct.BrandId = product.BrandId;
            existingProduct.ProductParentId = product.ProductParentId;
            existingProduct.Weight = product.Weight;
            existingProduct.Supply = product.Supply;
            existingProduct.Cost = product.Cost;
            existingProduct.Price = product.Price;

            _unitOfWork.Product.Edit(existingProduct);
            _unitOfWork.Commit();

            _fileService.UploadFiles(product.Files, existingProduct.Id, "Product");
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Produto editado com sucesso."
            };
        }

        public NotificationViewModel Create(ProductViewModel product)
        {
            var newProduct = new Product()
            {
                Id = Guid.NewGuid(),
                BrandId = product.BrandId,
                ProductGroupId = product.ProductGroupId,
                ProductParentId = product.ProductParentId,
                Name = product.Name,
                Code = product.Code,
                Supply = product.Supply,
                Weight = product.Weight,
                Cost = product.Cost,
                Price = product.Price
            };
            _unitOfWork.Product.Add(newProduct);
            _unitOfWork.Commit();
            _fileService.UploadFiles(product.Files, newProduct.Id, "Product");

            var result = new NotificationViewModel()
            {
                Message = "Produto criado com sucesso.",
                Status = true,
                Title = "Sucesso!"
            };
            return result;
        }
    }
}
